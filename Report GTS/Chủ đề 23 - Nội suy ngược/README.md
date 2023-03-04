# README

> Hướng dẫn sử dụng

## Cấu trúc chung

Chương trình gồm có một số modules chính

- Các module nội suy: Lagrange, Newton
- Module chính: InverseInterpolation

Các thiết lập cơ bản cho module được định nghĩa tại biến `config`

```json
{
    "method": "iterative",
    "inverse_function": {
        "type": "lagrange"
    },
    "iterative": {
        "type": "auto",
        "max_ite": 100,
        "tolerance": 0.000001
    }
}
```

Lựa chọn phương pháp nội suy bằng hàm ngược và phương pháp lặp tại đây.

Quy trình chạy

1. Định nghĩa một instance của class `InverseInterpolation`

   ```python
   client = InverseInterpolation(config)
   ```

2. Dữ liệu đầu vào cần chuẩn bị gồm có các mốc nội suy `X: Iterable` và `Y: Iterable`, thông thường là `List` hoặc `numpy array`. Giá trị `y: float` cần nội suy ngược

3. Có 2 phương thức có thể dùng để nội suy ngược

   ```python
   client.__fit_inverse_function__(self, X:Iterable, Y:Iterable, target: float)
   ```

   Phương thức này thực hiện nội suy ngược bằng phương pháp hàm ngược. Để thực hiện nội suy ngược bằng phương pháp lặp, dùng phương thức còn lại

   ```python
   client.__fit_iterative__(self, X: Iterable, Y:Iterable, target: float, method: str = 'auto')
   ```

   Phương thức này giống với phương pháp hàm ngược ở 3 tham số đầu. `arg` cuối cùng `method` có thể nhận 3 giá trị:

   - `auto `: thuật toán tự xác định tiến/lùi
   - `forward`: Sử dụng Newton tiến
   - `backward`: Sử dụng Newton lùi

   Kết quả trả về là một `json Object` chứa tất cả các nghiệm, số bước lặp, sai số và có hội tụ không.

   ```json
   [{'root': -0.1109693279996872,
     'iteration': 9,
     'tol': 7.680881881633184e-07,
     'isConvergence': True},
    {'root': 0.12717593781166284,
     'iteration': 7,
     'tol': 5.6558014716046046e-08,
     'isConvergence': True}]
   ```

   ---

   Chi tiết xem tại [Github](https://github.com/jurgendn/Numerical-Analysis)