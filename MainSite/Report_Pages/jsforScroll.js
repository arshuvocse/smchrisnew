var hidWidth;
var scrollBarWidths = 40;

var widthOfList = function () {
    var itemsWidth = 0;
    $('.list_colors.li1 li').each(function () {
        var itemWidth = $(this).outerWidth();
        itemsWidth += itemWidth;
    });
    return itemsWidth;
};

var widthOfHidden = function () {
    return (($('.wrapper_colors').outerWidth()) - widthOfList() - getLeftPosi()) - scrollBarWidths;
};

var getLeftPosi = function () {
    return $('.list_colors.li1').position().left;
};

var reAdjust = function () {
    if (($('.wrapper_colors').outerWidth()) < widthOfList()) {
        $('.scroller-right-1').show();
    }
    else {
        $('.scroller-right-1').hide();
    }

    if (getLeftPosi() < 0) {
        $('.scroller-left-1').show();
    }
    else {
        $('.item').animate({ left: "-=" + getLeftPosi() + "px" }, 'slow');
        $('.scroller-left-1').hide();
    }
}

reAdjust();

$(window).on('resize', function (e) {
    reAdjust();
});

$('.scroller-right-1').click(function () {

    $('.scroller-left-1').fadeIn('slow');
    $('.scroller-right-1').fadeOut('slow');

    $('.list_colors.li1').animate({ left: "+=" + widthOfHidden() + "px" }, 'slow', function () {

    });
});

$('.scroller-left-1').click(function () {

    $('.scroller-right-1').fadeIn('slow');
    $('.scroller-left-1').fadeOut('slow');

    $('.list_colors.li1').animate({ left: "-=" + getLeftPosi() + "px" }, 'slow', function () {

    });
});