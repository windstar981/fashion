(function ($) {
    "use strict";
    let current_url = window.location.href;

    // Dropdown on mouse hover
    $(document).ready(function () {
        function remove_slide() {
            console.log("vao day");
            let url = current_url.split('/');
            let size = url.length;
            if (!((size == 4 && url[size - 1] == '') || url.includes("home") || url.includes("Home"))) {
                console.log("vao trueeee");
                document.getElementById("header-carousel").remove();
            }

        }

        remove_slide();
        function toggleNavbarMethod() {
            if ($(window).width() > 992) {
                $('.navbar .dropdown').on('mouseover', function () {
                    $('.dropdown-toggle', this).trigger('click');
                }).on('mouseout', function () {
                    $('.dropdown-toggle', this).trigger('click').blur();
                });
            } else {
                $('.navbar .dropdown').off('mouseover').off('mouseout');
            }
        }
        toggleNavbarMethod();
        $(window).resize(toggleNavbarMethod);
    });
    
    
    // Back to top button
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('.back-to-top').fadeIn('slow');
        } else {
            $('.back-to-top').fadeOut('slow');
        }
    });
    $('.back-to-top').click(function () {
        $('html, body').animate({scrollTop: 0}, 1500, 'easeInOutExpo');
        return false;
    });


    // Vendor carousel
    $('.vendor-carousel').owlCarousel({
        loop: true,
        margin: 29,
        nav: false,
        autoplay: true,
        smartSpeed: 1000,
        responsive: {
            0:{
                items:2
            },
            576:{
                items:3
            },
            768:{
                items:4
            },
            992:{
                items:5
            },
            1200:{
                items:6
            }
        }
    });


    // Related carousel
    $('.related-carousel').owlCarousel({
        loop: true,
        margin: 29,
        nav: false,
        autoplay: true,
        smartSpeed: 1000,
        responsive: {
            0:{
                items:1
            },
            576:{
                items:2
            },
            768:{
                items:3
            },
            992:{
                items:4
            }
        }
    });


    // Product Quantity
    $('.quantity button').on('click', function () {
        var button = $(this);
        var oldValue = button.parent().parent().find('input').val();
        if (button.hasClass('btn-plus')) {
            var newVal = parseFloat(oldValue) + 1;
        } else {
            if (oldValue > 0) {
                var newVal = parseFloat(oldValue) - 1;
            } else {
                newVal = 0;
            }
        }
        button.parent().parent().find('input').val(newVal);
    });
    $('.add-to-cart-btn').click(function () {
        var productId = $(this).attr('product-id');
        $.ajax({
            url: '/Carts/AddToCart/' + productId,
            method: 'GET',
            success: function (response) {
                if (response.success) {
                    $.notify("Thêm vào giỏ hàng thành công!", "success");
                    // Redirect tới trang chủ hoặc bất kỳ trang nào bạn muốn ở đây
                    //window.location.href = '/';
                } else {
                    $.notify(response.content, "warn");
                    window.location.href = 'http://localhost:5074/Home/Login';
                }
            },
            error: function () {
                alert('Có lỗi xảy ra khi thêm vào giỏ hàng.');
            }
        });
    });

    $('.btn-test').click(function () {
        console.log(1234);
        var dataToSend = {
            name: 'hieu',
            age: 18,
            address: 'YourAddressHere'
        };

        $.ajax({
            url: '/Home/Test',
            data: JSON.stringify(dataToSend),
            method: 'POST',
            contentType: 'application/json',
            success: function (response) {
                console.log(response);
            },
            error: function () {
                alert('Có lỗi xảy ra khi thêm vào giỏ hàng.');
            }
        });
    });
})(jQuery);

