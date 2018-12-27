new WOW().init();

$(document).on('click', '.nav-link', function (e) {
    
    $([document.documentElement, document.body]).animate({
        scrollTop: $($(this).attr('href')).offset().top
    }, 500);
    
});

$(document).on('click', 'a[href="#about"]', function (e) {
    e.preventDefault();
    $([document.documentElement, document.body]).animate({
        scrollTop: $($(this).attr('href')).offset().top
    }, 500);

});
