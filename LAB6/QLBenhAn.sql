DROP SCHEMA IF EXISTS qlbenhan CASCADE;
CREATE SCHEMA qlbenhan;
SET search_path TO qlbenhan;
 
 
CREATE TABLE benh_nhan(
  ma_bn      INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  ma_ho_so   VARCHAR(12) GENERATED ALWAYS AS ('HS' || LPAD(ma_bn::TEXT, 6, '0')) STORED,
  ho_ten     VARCHAR(100) NOT NULL,
  ngay_sinh  DATE NOT NULL,
  dia_chi    VARCHAR(200),
  dien_thoai VARCHAR(15) NOT NULL,
  anh_3x4    BYTEA,
  ngay_cap   DATE NOT NULL DEFAULT CURRENT_DATE,
  CONSTRAINT uq_benh_nhan_thong_tin UNIQUE (ho_ten, ngay_sinh, dien_thoai)
);
CREATE UNIQUE INDEX ux_benh_nhan_ma_ho_so ON benh_nhan(ma_ho_so);
 
CREATE TABLE le_phi_nam(
  ma_le_phi    INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  ma_bn        INT NOT NULL REFERENCES benh_nhan(ma_bn),
  nam_ap_dung  INT NOT NULL,
  so_tien      NUMERIC(18,0) NOT NULL CHECK (so_tien > 0),
  ngay_dong    DATE NOT NULL,
  ngay_het_han DATE NOT NULL,
  CONSTRAINT uq_le_phi UNIQUE (ma_bn, nam_ap_dung)
);
 
CREATE TABLE bac_si(
  ma_bs      INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  ho_ten     VARCHAR(100) NOT NULL,
  chuyen_mon VARCHAR(100),
  khoa       VARCHAR(100) NOT NULL
);
 
CREATE TABLE thuoc(
  ma_thuoc    INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  ten_thuoc   VARCHAR(100) NOT NULL UNIQUE,
  don_vi_tinh VARCHAR(20),
  hoat_chat   VARCHAR(100),
  ton_kho     INT NOT NULL DEFAULT 0 CHECK (ton_kho >= 0)
);
 
CREATE TABLE phieu_kham(
  ma_phieu_kham INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  ma_bn         INT NOT NULL REFERENCES benh_nhan(ma_bn),   
  ma_bs         INT NOT NULL REFERENCES bac_si(ma_bs),      
  ngay_gio_kham TIMESTAMP NOT NULL,
  loai_kham     VARCHAR(20) NOT NULL CHECK (loai_kham IN ('DieuTri', 'DinhKy')),
  khoa          VARCHAR(100) NOT NULL,
  trieu_chung   VARCHAR(500),     
  chan_doan     VARCHAR(500),
  ngay_hen      TIMESTAMP NULL,   
  trang_thai    VARCHAR(20) NOT NULL DEFAULT 'DangKham'
                CHECK (trang_thai IN ('DaHen', 'DangKham', 'ChoThanhToan', 'HoanTat', 'DaHuy')),
  nam_hen INT GENERATED ALWAYS AS (EXTRACT(YEAR FROM ngay_hen)::INT) STORED,
  quy_hen INT GENERATED ALWAYS AS (EXTRACT(QUARTER FROM ngay_hen)::INT) STORED,
  CONSTRAINT ck_dinh_ky_co_ngay_hen CHECK (loai_kham = 'DieuTri' OR ngay_hen IS NOT NULL)
);
CREATE UNIQUE INDEX ux_phieu_kham_lich_quy ON phieu_kham(ma_bn, nam_hen, quy_hen)
  WHERE loai_kham = 'DinhKy' AND trang_thai <> 'DaHuy';
 
CREATE TABLE phieu_xet_nghiem(
  ma_phieu_xn   INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  ma_phieu_kham INT NOT NULL REFERENCES phieu_kham(ma_phieu_kham),
  ten_xn        VARCHAR(100) NOT NULL,
  ngay_xn       TIMESTAMP NULL,
  ket_qua       VARCHAR(500) NULL,
  trang_thai    VARCHAR(20) NOT NULL DEFAULT 'ChoThucHien'
                CHECK (trang_thai IN ('ChoThucHien', 'DaCoKetQua'))
);
 
CREATE TABLE toa_thuoc(
  ma_toa        INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  ma_phieu_kham INT NOT NULL UNIQUE REFERENCES phieu_kham(ma_phieu_kham),  
  ngay_lap      TIMESTAMP NOT NULL DEFAULT NOW(),
  loi_dan       VARCHAR(500)
);
 
CREATE TABLE chi_tiet_toa_thuoc(
  ma_toa            INT NOT NULL REFERENCES toa_thuoc(ma_toa),
  ma_thuoc          INT NOT NULL REFERENCES thuoc(ma_thuoc),
  so_luong          INT NOT NULL CHECK (so_luong > 0),
  lieu_dung         VARCHAR(100),
  cach_dung         VARCHAR(200),
  ghi_chu_phan_ung  VARCHAR(300),   
  PRIMARY KEY (ma_toa, ma_thuoc)
);
 
CREATE TABLE hoa_don_vien_phi(
  ma_hd          INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
  ma_phieu_kham  INT NOT NULL UNIQUE REFERENCES phieu_kham(ma_phieu_kham),
  tong_tien      NUMERIC(18,0) NOT NULL CHECK (tong_tien >= 0),
  ngay_thanh_toan TIMESTAMP NULL,
  trang_thai     VARCHAR(20) NOT NULL DEFAULT 'ChuaThanhToan'
                 CHECK (trang_thai IN ('ChuaThanhToan', 'DaThanhToan'))
);
 
/* ---------- 2. TRIGGER: phiếu đã hoàn tất thì không được sửa ---------- */
CREATE FUNCTION trg_phieu_kham_khoa_khi_hoan_tat() RETURNS TRIGGER AS $$
BEGIN
  IF OLD.trang_thai = 'HoanTat' THEN
    RAISE EXCEPTION 'Phiếu khám đã hoàn tất, không được sửa';
  END IF;
  RETURN NEW;
END;
$$ LANGUAGE plpgsql;
 
CREATE TRIGGER trg_phieu_kham_khoa
BEFORE UPDATE ON phieu_kham
FOR EACH ROW EXECUTE FUNCTION trg_phieu_kham_khoa_khi_hoan_tat();
 
/* ---------- 3. HÀM: kiểm tra lệ phí còn hạn ---------- */
CREATE FUNCTION fn_le_phi_con_han(p_ma_bn INT) RETURNS BOOLEAN AS $$
  SELECT EXISTS(SELECT 1 FROM le_phi_nam
                WHERE ma_bn = p_ma_bn AND ngay_het_han >= CURRENT_DATE);
$$ LANGUAGE sql STABLE;
 
/* ---------- 4. HÀM NGHIỆP VỤ ---------- */
 
CREATE FUNCTION sp_dang_ky_lan_dau(
  p_ho_ten VARCHAR, p_ngay_sinh DATE, p_dia_chi VARCHAR,
  p_dien_thoai VARCHAR, p_so_tien NUMERIC, p_anh BYTEA DEFAULT NULL
) RETURNS VARCHAR AS $$
DECLARE
  v_ma_bn INT;
  v_ma_ho_so VARCHAR;
BEGIN
  IF p_so_tien IS NULL THEN
    RAISE EXCEPTION 'Chưa đóng lệ phí năm, không thể cấp hồ sơ';
  END IF;
  IF EXISTS(SELECT 1 FROM benh_nhan
            WHERE ho_ten = p_ho_ten AND ngay_sinh = p_ngay_sinh AND dien_thoai = p_dien_thoai) THEN
    RAISE EXCEPTION 'Bệnh nhân đã có hồ sơ';
  END IF;
 
  INSERT INTO benh_nhan(ho_ten, ngay_sinh, dia_chi, dien_thoai, anh_3x4)
  VALUES (p_ho_ten, p_ngay_sinh, p_dia_chi, p_dien_thoai, p_anh)
  RETURNING ma_bn INTO v_ma_bn;
 
  INSERT INTO le_phi_nam(ma_bn, nam_ap_dung, so_tien, ngay_dong, ngay_het_han)
  VALUES (v_ma_bn, EXTRACT(YEAR FROM CURRENT_DATE)::INT, p_so_tien,
          CURRENT_DATE, (CURRENT_DATE + INTERVAL '1 year')::DATE);
 
  SELECT ma_ho_so INTO v_ma_ho_so FROM benh_nhan WHERE ma_bn = v_ma_bn;
  RETURN v_ma_ho_so;
END;
$$ LANGUAGE plpgsql;
 
-- 4.2 Gia hạn lệ phí năm
CREATE FUNCTION sp_gia_han_le_phi(p_ma_bn INT, p_so_tien NUMERIC) RETURNS VOID AS $$
DECLARE
  v_het_han DATE;
  v_tu DATE;
BEGIN
  IF NOT EXISTS(SELECT 1 FROM benh_nhan WHERE ma_bn = p_ma_bn) THEN
    RAISE EXCEPTION 'Bệnh nhân không tồn tại';
  END IF;
  SELECT MAX(ngay_het_han) INTO v_het_han FROM le_phi_nam WHERE ma_bn = p_ma_bn;
  -- còn hạn: nối tiếp từ ngày hết hạn; hết hạn: tính từ hôm nay
  v_tu := CASE WHEN v_het_han >= CURRENT_DATE THEN v_het_han ELSE CURRENT_DATE END;
  INSERT INTO le_phi_nam(ma_bn, nam_ap_dung, so_tien, ngay_dong, ngay_het_han)
  VALUES (p_ma_bn, EXTRACT(YEAR FROM v_tu)::INT, p_so_tien,
          CURRENT_DATE, (v_tu + INTERVAL '1 year')::DATE);
END;
$$ LANGUAGE plpgsql;
 
-- 4.3 Tạo phiếu khám điều trị (yêu cầu lệ phí còn hạn). Trả về mã phiếu.
CREATE FUNCTION sp_tao_phieu_kham(
  p_ma_bn INT, p_ma_bs INT, p_khoa VARCHAR, p_trieu_chung VARCHAR DEFAULT NULL
) RETURNS INT AS $$
DECLARE v_ma INT;
BEGIN
  IF NOT fn_le_phi_con_han(p_ma_bn) THEN
    RAISE EXCEPTION 'Lệ phí năm đã hết hạn, yêu cầu gia hạn';
  END IF;
  INSERT INTO phieu_kham(ma_bn, ma_bs, ngay_gio_kham, loai_kham, khoa, trieu_chung, trang_thai)
  VALUES (p_ma_bn, p_ma_bs, NOW(), 'DieuTri', p_khoa, p_trieu_chung, 'DangKham')
  RETURNING ma_phieu_kham INTO v_ma;
  RETURN v_ma;
END;
$$ LANGUAGE plpgsql;
 
-- 4.4 Đặt lịch khám định kỳ theo quý (miễn phí). Trả về mã phiếu.
CREATE FUNCTION sp_dat_lich_dinh_ky(
  p_ma_bn INT, p_ma_bs INT, p_khoa VARCHAR, p_ngay_hen TIMESTAMP
) RETURNS INT AS $$
DECLARE v_ma INT;
BEGIN
  IF NOT fn_le_phi_con_han(p_ma_bn) THEN
    RAISE EXCEPTION 'Lệ phí năm đã hết hạn, yêu cầu gia hạn';
  END IF;
  IF p_ngay_hen <= NOW() THEN
    RAISE EXCEPTION 'Ngày hẹn phải lớn hơn thời điểm hiện tại';
  END IF;
  BEGIN
    INSERT INTO phieu_kham(ma_bn, ma_bs, ngay_gio_kham, loai_kham, khoa, ngay_hen, trang_thai)
    VALUES (p_ma_bn, p_ma_bs, p_ngay_hen, 'DinhKy', p_khoa, p_ngay_hen, 'DaHen')
    RETURNING ma_phieu_kham INTO v_ma;
  EXCEPTION WHEN unique_violation THEN
    RAISE EXCEPTION 'Bệnh nhân đã có lịch khám định kỳ trong quý này';
  END;
  RETURN v_ma;
END;
$$ LANGUAGE plpgsql;
 
-- 4.5 Chỉ định xét nghiệm. Trả về mã phiếu xét nghiệm.
CREATE FUNCTION sp_chi_dinh_xn(p_ma_phieu_kham INT, p_ten_xn VARCHAR) RETURNS INT AS $$
DECLARE v_ma INT;
BEGIN
  IF EXISTS(SELECT 1 FROM phieu_kham WHERE ma_phieu_kham = p_ma_phieu_kham AND trang_thai = 'HoanTat') THEN
    RAISE EXCEPTION 'Phiếu khám đã hoàn tất, không được thay đổi';
  END IF;
  -- phiếu không tồn tại => lỗi khóa ngoại
  INSERT INTO phieu_xet_nghiem(ma_phieu_kham, ten_xn) VALUES (p_ma_phieu_kham, p_ten_xn)
  RETURNING ma_phieu_xn INTO v_ma;
  RETURN v_ma;
END;
$$ LANGUAGE plpgsql;
 
-- 4.6 Nhập kết quả xét nghiệm
CREATE FUNCTION sp_nhap_ket_qua_xn(p_ma_phieu_xn INT, p_ket_qua VARCHAR) RETURNS VOID AS $$
BEGIN
  UPDATE phieu_xet_nghiem
  SET ket_qua = p_ket_qua, ngay_xn = NOW(), trang_thai = 'DaCoKetQua'
  WHERE ma_phieu_xn = p_ma_phieu_xn;
  IF NOT FOUND THEN
    RAISE EXCEPTION 'Phiếu xét nghiệm không tồn tại';
  END IF;
END;
$$ LANGUAGE plpgsql;
 
-- 4.7 Lập toa thuốc: toa_thuoc + chi_tiet_toa_thuoc + trừ tồn kho (1 transaction).
CREATE FUNCTION sp_lap_toa(
  p_ma_phieu_kham INT, p_loi_dan VARCHAR, p_ds JSONB
) RETURNS INT AS $$
DECLARE
  v_tt VARCHAR;
  v_loai VARCHAR;
  v_ma_toa INT;
BEGIN
  SELECT trang_thai, loai_kham INTO v_tt, v_loai
  FROM phieu_kham WHERE ma_phieu_kham = p_ma_phieu_kham FOR UPDATE;
 
  IF NOT FOUND THEN RAISE EXCEPTION 'Phiếu khám không tồn tại'; END IF;
  IF v_tt = 'HoanTat' THEN RAISE EXCEPTION 'Phiếu khám đã hoàn tất, không được lập toa'; END IF;
  IF EXISTS(SELECT 1 FROM toa_thuoc WHERE ma_phieu_kham = p_ma_phieu_kham) THEN
    RAISE EXCEPTION 'Phiếu khám đã có toa thuốc';
  END IF;
  IF p_ds IS NULL OR jsonb_typeof(p_ds) <> 'array' OR jsonb_array_length(p_ds) = 0 THEN
    RAISE EXCEPTION 'Toa thuốc phải có ít nhất 1 thuốc';
  END IF;
 
  -- khóa các dòng thuốc để tránh 2 bác sĩ cùng trừ tồn kho một lúc
  PERFORM 1 FROM thuoc
  WHERE ma_thuoc IN (SELECT d.ma_thuoc FROM jsonb_to_recordset(p_ds) AS d(ma_thuoc INT))
  FOR UPDATE;
 
  IF EXISTS(SELECT 1
            FROM jsonb_to_recordset(p_ds) AS d(ma_thuoc INT, so_luong INT)
            LEFT JOIN thuoc t ON t.ma_thuoc = d.ma_thuoc
            WHERE t.ma_thuoc IS NULL OR t.ton_kho < d.so_luong) THEN
    RAISE EXCEPTION 'Thuốc không tồn tại hoặc vượt quá tồn kho';
  END IF;
 
  INSERT INTO toa_thuoc(ma_phieu_kham, loi_dan) VALUES (p_ma_phieu_kham, p_loi_dan)
  RETURNING ma_toa INTO v_ma_toa;
 
  INSERT INTO chi_tiet_toa_thuoc(ma_toa, ma_thuoc, so_luong, lieu_dung, cach_dung, ghi_chu_phan_ung)
  SELECT v_ma_toa, d.ma_thuoc, d.so_luong, d.lieu_dung, d.cach_dung, d.ghi_chu_phan_ung
  FROM jsonb_to_recordset(p_ds) AS d(ma_thuoc INT, so_luong INT, lieu_dung TEXT,
                                     cach_dung TEXT, ghi_chu_phan_ung TEXT);
 
  UPDATE thuoc t SET ton_kho = t.ton_kho - d.so_luong
  FROM jsonb_to_recordset(p_ds) AS d(ma_thuoc INT, so_luong INT)
  WHERE t.ma_thuoc = d.ma_thuoc;
 
  IF v_loai = 'DieuTri' THEN
    UPDATE phieu_kham SET trang_thai = 'ChoThanhToan' WHERE ma_phieu_kham = p_ma_phieu_kham;
  END IF;
  RETURN v_ma_toa;
END;
$$ LANGUAGE plpgsql;
 
-- 4.8 Thanh toán viện phí. Trả về mã hóa đơn.
CREATE FUNCTION sp_thanh_toan(p_ma_phieu_kham INT, p_tong_tien NUMERIC) RETURNS INT AS $$
DECLARE v_ma INT;
BEGIN
  IF NOT EXISTS(SELECT 1 FROM phieu_kham WHERE ma_phieu_kham = p_ma_phieu_kham) THEN
    RAISE EXCEPTION 'Phiếu khám không tồn tại';
  END IF;
  IF EXISTS(SELECT 1 FROM hoa_don_vien_phi
            WHERE ma_phieu_kham = p_ma_phieu_kham AND trang_thai = 'DaThanhToan') THEN
    RAISE EXCEPTION 'Phiếu khám đã thanh toán';
  END IF;
  INSERT INTO hoa_don_vien_phi(ma_phieu_kham, tong_tien, ngay_thanh_toan, trang_thai)
  VALUES (p_ma_phieu_kham, p_tong_tien, NOW(), 'DaThanhToan')
  RETURNING ma_hd INTO v_ma;
  RETURN v_ma;
END;
$$ LANGUAGE plpgsql;
 
-- 4.9 Hoàn tất phiếu khám (khám điều trị phải thanh toán trước; khám định kỳ miễn phí)
CREATE FUNCTION sp_hoan_tat_phieu(p_ma_phieu_kham INT) RETURNS VOID AS $$
DECLARE
  v_tt VARCHAR;
  v_loai VARCHAR;
BEGIN
  SELECT trang_thai, loai_kham INTO v_tt, v_loai
  FROM phieu_kham WHERE ma_phieu_kham = p_ma_phieu_kham;
  IF NOT FOUND THEN RAISE EXCEPTION 'Phiếu khám không tồn tại'; END IF;
  IF v_tt = 'HoanTat' THEN RAISE EXCEPTION 'Phiếu khám đã hoàn tất'; END IF;
  IF v_loai = 'DieuTri' AND NOT EXISTS(
       SELECT 1 FROM hoa_don_vien_phi
       WHERE ma_phieu_kham = p_ma_phieu_kham AND trang_thai = 'DaThanhToan') THEN
    RAISE EXCEPTION 'Chưa thanh toán viện phí, không thể hoàn tất';
  END IF;
  UPDATE phieu_kham SET trang_thai = 'HoanTat' WHERE ma_phieu_kham = p_ma_phieu_kham;
END;
$$ LANGUAGE plpgsql;
 
/* ---------- 5. DỮ LIỆU MẪU ---------- */
INSERT INTO bac_si(ho_ten, chuyen_mon, khoa) VALUES
 ('Trần Minh Khoa', 'Nội tổng quát', 'Nội tổng quát'),
 ('Lê Thu Hà',      'Tim mạch',      'Tim mạch');
INSERT INTO thuoc(ten_thuoc, don_vi_tinh, hoat_chat, ton_kho) VALUES
 ('Paracetamol 500mg', 'Viên', 'Paracetamol', 100),
 ('Amoxicillin 500mg', 'Viên', 'Amoxicillin', 20),
 ('Vitamin C 500mg',   'Viên', 'Acid ascorbic', 5);
 
DO $$
DECLARE
  v_hs VARCHAR; v_bn INT; v_pk INT; v_pk2 INT; v_pk_dk INT; v_toa INT; v_tmp INT;
  v_ds JSONB := '[{"ma_thuoc":1,"so_luong":10,"lieu_dung":"2 viên/lần","cach_dung":"Uống sau ăn"}]';
  v_ds_qua_ton JSONB := '[{"ma_thuoc":3,"so_luong":999,"lieu_dung":"1 viên/ngày","cach_dung":"Uống sáng"}]';
  v_h1 TIMESTAMP := NOW() + INTERVAL '7 days';
  v_h2 TIMESTAMP := NOW() + INTERVAL '8 days';
  v_n INT; v_s VARCHAR;
BEGIN
  -- TC1: đăng ký lần đầu hợp lệ
  BEGIN
    v_hs := sp_dang_ky_lan_dau('Nguyễn Văn A', '2000-01-15', 'Quận 1, TP.HCM', '0901000001', 200000);
    SELECT ma_bn INTO v_bn FROM benh_nhan WHERE ma_ho_so = v_hs;
    RAISE NOTICE 'TC1 ĐẠT - Mã hồ sơ: %', v_hs;
  EXCEPTION WHEN OTHERS THEN RAISE NOTICE 'TC1 LỖI: %', SQLERRM; END;
 
  -- TC2: đăng ký trùng họ tên + ngày sinh + SĐT
  BEGIN
    v_hs := sp_dang_ky_lan_dau('Nguyễn Văn A', '2000-01-15', 'Quận 1', '0901000001', 200000);
    RAISE NOTICE 'TC2 SAI: lẽ ra phải báo lỗi';
  EXCEPTION WHEN OTHERS THEN RAISE NOTICE 'TC2 ĐẠT - %', SQLERRM; END;
 
  -- TC3: lỗi giữa chừng (lệ phí = 0 vi phạm CHECK sau khi đã thêm bệnh nhân) => rollback cả hai bảng
  BEGIN
    v_hs := sp_dang_ky_lan_dau('Trần Thị B', '1995-05-20', 'Quận 3', '0901000002', 0);
    RAISE NOTICE 'TC3 SAI: lẽ ra phải lỗi';
  EXCEPTION WHEN OTHERS THEN
    SELECT COUNT(*) INTO v_n FROM benh_nhan WHERE ho_ten = 'Trần Thị B';
    RAISE NOTICE 'TC3 ĐẠT - % | Số BN "Trần Thị B" còn lại (phải = 0): %', SQLERRM, v_n;
  END;
 
  -- TC4: chưa đóng lệ phí (NULL) => không cấp hồ sơ
  BEGIN
    v_hs := sp_dang_ky_lan_dau('Lê Văn C', '1990-03-03', 'Quận 5', '0901000003', NULL);
    RAISE NOTICE 'TC4 SAI: lẽ ra phải từ chối';
  EXCEPTION WHEN OTHERS THEN RAISE NOTICE 'TC4 ĐẠT - %', SQLERRM; END;
 
  -- TC5: tra cứu -> xem 3 câu SELECT ở cuối file
 
  -- Giả lập lệ phí hết hạn
  UPDATE le_phi_nam SET nam_ap_dung = 2025, ngay_dong = '2025-01-01', ngay_het_han = '2025-12-31'
  WHERE ma_bn = v_bn;
 
  -- TC6: khám khi lệ phí hết hạn => yêu cầu gia hạn
  BEGIN
    v_pk := sp_tao_phieu_kham(v_bn, 1, 'Nội tổng quát', 'Nhức đầu');
    RAISE NOTICE 'TC6 SAI: lẽ ra phải chặn';
  EXCEPTION WHEN OTHERS THEN RAISE NOTICE 'TC6 ĐẠT - %', SQLERRM; END;
 
  -- TC8: đặt lịch định kỳ khi lệ phí hết hạn => từ chối
  BEGIN
    v_pk_dk := sp_dat_lich_dinh_ky(v_bn, 1, 'Khám sức khỏe', v_h1);
    RAISE NOTICE 'TC8 SAI: lẽ ra phải từ chối';
  EXCEPTION WHEN OTHERS THEN RAISE NOTICE 'TC8 ĐẠT - %', SQLERRM; END;
 
  -- Gia hạn lệ phí rồi khám lại
  PERFORM sp_gia_han_le_phi(v_bn, 200000);
  RAISE NOTICE 'Đã gia hạn lệ phí. Còn hạn = %', fn_le_phi_con_han(v_bn);
 
  -- TC7: đặt lịch định kỳ 2 lần trong cùng 1 quý
  BEGIN
    v_pk_dk := sp_dat_lich_dinh_ky(v_bn, 1, 'Khám sức khỏe', v_h1);
    RAISE NOTICE 'TC7 lần 1 ĐẠT - đặt lịch thành công, mã phiếu %', v_pk_dk;
  EXCEPTION WHEN OTHERS THEN RAISE NOTICE 'TC7 lần 1 LỖI: %', SQLERRM; END;
  BEGIN
    v_tmp := sp_dat_lich_dinh_ky(v_bn, 1, 'Khám sức khỏe', v_h2);
    RAISE NOTICE 'TC7 lần 2 SAI: lẽ ra phải báo trùng quý (nếu +7 và +8 ngày rơi vào 2 quý khác nhau thì đổi ngày)';
  EXCEPTION WHEN OTHERS THEN RAISE NOTICE 'TC7 lần 2 ĐẠT - %', SQLERRM; END;
 
  -- TC9: khám định kỳ => không thu phí, không cần toa, hoàn tất trực tiếp
  BEGIN
    PERFORM sp_hoan_tat_phieu(v_pk_dk);
    SELECT trang_thai INTO v_s FROM phieu_kham WHERE ma_phieu_kham = v_pk_dk;
    SELECT COUNT(*) INTO v_n FROM toa_thuoc WHERE ma_phieu_kham = v_pk_dk;
    RAISE NOTICE 'TC9 ĐẠT - khám định kỳ hoàn tất, trạng thái: %, số toa = %', v_s, v_n;
  EXCEPTION WHEN OTHERS THEN RAISE NOTICE 'TC9 LỖI: %', SQLERRM; END;
 
  -- Tạo phiếu khám điều trị bình thường
  v_pk := sp_tao_phieu_kham(v_bn, 1, 'Nội tổng quát', 'Nhức đầu, sốt 38.5, huyết áp 120/80');
 
  -- TC10: xét nghiệm / toa cho phiếu không tồn tại => lỗi
  BEGIN
    v_tmp := sp_chi_dinh_xn(99999, 'Công thức máu');
    RAISE NOTICE 'TC10a SAI: lẽ ra phải lỗi khóa ngoại';
  EXCEPTION WHEN OTHERS THEN RAISE NOTICE 'TC10a ĐẠT - %', SQLERRM; END;
  BEGIN
    v_tmp := sp_lap_toa(99999, 'Nghỉ ngơi', v_ds);
    RAISE NOTICE 'TC10b SAI: lẽ ra phải lỗi';
  EXCEPTION WHEN OTHERS THEN RAISE NOTICE 'TC10b ĐẠT - %', SQLERRM; END;
 
  -- Luồng bình thường: chỉ định xét nghiệm, nhập kết quả
  v_tmp := sp_chi_dinh_xn(v_pk, 'Công thức máu');
  PERFORM sp_nhap_ket_qua_xn(v_tmp, 'Bình thường');
 
  -- TC11: lập toa lần 1 thành công, lần 2 cho cùng phiếu bị chặn
  BEGIN
    v_toa := sp_lap_toa(v_pk, 'Uống nhiều nước, nghỉ ngơi', v_ds);
    SELECT ton_kho INTO v_n FROM thuoc WHERE ma_thuoc = 1;
    RAISE NOTICE 'TC11 lần 1 ĐẠT - mã toa %, tồn kho Paracetamol còn: % (phải = 90)', v_toa, v_n;
  EXCEPTION WHEN OTHERS THEN RAISE NOTICE 'TC11 lần 1 LỖI: %', SQLERRM; END;
  BEGIN
    v_tmp := sp_lap_toa(v_pk, 'Toa thứ 2', v_ds);
    RAISE NOTICE 'TC11 lần 2 SAI: lẽ ra phải chặn';
  EXCEPTION WHEN OTHERS THEN RAISE NOTICE 'TC11 lần 2 ĐẠT - %', SQLERRM; END;
 
  -- TC12: kê thuốc vượt tồn kho => từ chối, rollback toa
  v_pk2 := sp_tao_phieu_kham(v_bn, 2, 'Tim mạch', 'Đau ngực');
  BEGIN
    v_tmp := sp_lap_toa(v_pk2, 'Test vượt tồn', v_ds_qua_ton);
    RAISE NOTICE 'TC12 SAI: lẽ ra phải từ chối';
  EXCEPTION WHEN OTHERS THEN
    SELECT COUNT(*) INTO v_n FROM toa_thuoc WHERE ma_phieu_kham = v_pk2;
    RAISE NOTICE 'TC12 ĐẠT - % | Số toa của phiếu (phải = 0): %', SQLERRM, v_n;
  END;
 
  -- TC13: hoàn tất khi chưa thanh toán => từ chối
  BEGIN
    PERFORM sp_hoan_tat_phieu(v_pk);
    RAISE NOTICE 'TC13 SAI: lẽ ra phải từ chối';
  EXCEPTION WHEN OTHERS THEN RAISE NOTICE 'TC13 ĐẠT - %', SQLERRM; END;
 
  -- TC14: thanh toán xong -> hoàn tất -> không sửa được nữa
  BEGIN
    v_tmp := sp_thanh_toan(v_pk, 350000);
    PERFORM sp_hoan_tat_phieu(v_pk);
    SELECT trang_thai INTO v_s FROM phieu_kham WHERE ma_phieu_kham = v_pk;
    RAISE NOTICE 'TC14a ĐẠT - trạng thái: %', v_s;
  EXCEPTION WHEN OTHERS THEN RAISE NOTICE 'TC14a LỖI: %', SQLERRM; END;
  BEGIN
    UPDATE phieu_kham SET chan_doan = 'Sửa sau khi hoàn tất' WHERE ma_phieu_kham = v_pk;
    RAISE NOTICE 'TC14b SAI: lẽ ra phải chặn sửa';
  EXCEPTION WHEN OTHERS THEN RAISE NOTICE 'TC14b ĐẠT - %', SQLERRM; END;
END $$;
 
-- TC5: tra cứu bằng mã hồ sơ / SĐT / họ tên
SELECT 'TC5 - theo mã hồ sơ' AS tra_cuu, ma_ho_so, ho_ten, dien_thoai FROM benh_nhan WHERE ma_ho_so = 'HS000001';
SELECT 'TC5 - theo SĐT' AS tra_cuu, ma_ho_so, ho_ten, dien_thoai FROM benh_nhan WHERE dien_thoai = '0901000001';
SELECT 'TC5 - theo họ tên' AS tra_cuu, ma_ho_so, ho_ten, dien_thoai FROM benh_nhan WHERE ho_ten ILIKE '%Văn A%';
 
-- Lịch sử bệnh án của bệnh nhân HS000001
SELECT pk.ma_phieu_kham, pk.ngay_gio_kham, pk.loai_kham, pk.khoa, bs.ho_ten AS bac_si,
       pk.trieu_chung, pk.chan_doan, pk.trang_thai
FROM phieu_kham pk JOIN bac_si bs ON bs.ma_bs = pk.ma_bs
WHERE pk.ma_bn = 1 ORDER BY pk.ngay_gio_kham;