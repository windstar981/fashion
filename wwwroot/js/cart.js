$(document).ready(function () {
    $('.btn-plus').click(function () {
        var clickedButton = $(this);
        var productId = $(this).attr('product-id');
        let quantity = $(this).closest('td').find('.quantity-product').val();
        let price = $(this).attr('price-product');
        let newPrice = quantity * price;
        $.ajax({
            url: '/Carts/AddToCart/',
            data: {
                id: productId,
                type: 'plus'
            },
            method: 'GET',
            success: function (response) {
                if (response.success) {
                    $.notify("Tăng thành công!", "success");
                    //console.log($(this).closest('tr').find('.total-price').text(newPrice));
                    //$(this).closest('tr').find('.total-price').text(newPrice);
                    //console.log(newPrice);
                    var totalPriceCell = clickedButton.closest('tr').find('.total-price');
                    totalPriceCell.text('$' + newPrice);
                    // Redirect tới trang chủ hoặc bất kỳ trang nào bạn muốn ở đây
                    //window.location.href = '/';
                } else {
                    $.notify(response.content, "warn");
                }
            },
            error: function () {
                alert('Có lỗi xảy ra khi thêm vào giỏ hàng.');
            }
        });
    });
    $('.btn-minus').click(function () {
        let productId = $(this).attr('product-id');
        $.ajax({
            url: '/Carts/AddToCart/' + productId,
            data: {
                id: productId,
                type: 'minus'
            },
            method: 'GET',
            success: function (response) {
                if (response.success) {
                    $.notify("Giảm thành công!", "success");
                    // Redirect tới trang chủ hoặc bất kỳ trang nào bạn muốn ở đây
                    //window.location.href = '/';
                } else {
                    $.notify(response.content, "warn");
                }
            },
            error: function () {
                alert('Có lỗi xảy ra khi thêm vào giỏ hàng.');
            }
        });
    });
});
