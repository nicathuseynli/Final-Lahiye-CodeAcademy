var $offcanvasNav = $('.mobile-menu, .offcanvas-minicart_menu'),
$offcanvasNavWrap = $(
    '.mobile-menu_wrapper, .offcanvas-minicart_wrapperm, .offcanvas-search_wrapper'
),
$offcanvasNavSubMenu = $offcanvasNav.find('.sub-menu'),
$menuToggle = $('.menu-btn'),
$menuClose = $('.button-close');

$offcanvasNavSubMenu.slideUp();

$offcanvasNav.on('click', 'li a, li .menu-expand', function (e) {
var $this = $(this);
if (
    $this
        .parent()
        .attr('class')
        .match(
            /\b(menu-item-has-children|has-children|has-sub-menu)\b/
        ) &&
    ($this.attr('href') === '#' ||
        $this.attr('href') === '' ||
        $this.hasClass('menu-expand'))
) {
    e.preventDefault();
    if ($this.siblings('ul:visible').length) {
        $this.siblings('ul').slideUp('slow');
    } else {
        $this
            .closest('li')
            .siblings('li')
            .find('ul:visible')
            .slideUp('slow');
        $this.closest('li').siblings('li').removeClass('menu-open');
        $this.siblings('ul').slideDown('slow');
        $this.parent().siblings().children('ul').slideUp();
    }
}
if (
    $this.is('a') ||
    $this.is('span') ||
    $this.attr('class').match(/\b(menu-expand)\b/)
) {
    $this.parent().toggleClass('menu-open');
} else if (
    $this.is('li') &&
    $this.attr('class').match(/\b('menu-item-has-children')\b/)
) {
    $this.toggleClass('menu-open');
}
});

$('.button-close').on('click', function (e) {
e.preventDefault();
$('.mobile-menu .sub-menu').slideUp();
$('.mobile-menu .menu-item-has-children').removeClass('menu-open');
});
