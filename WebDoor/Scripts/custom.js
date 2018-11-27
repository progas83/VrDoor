jQuery(document).ready(function ($) {
var myFullpage = new fullpage('#fullpage', {
        verticalCentered: false,
        css3:false
    });

  $('#games-slider').slick({
      infinite: true,
      slidesToShow: 2,
      autoplay: true,
      autoplaySpeed: 1400,
      slidesToScroll: 1,
      accessibility: true,
      arrows: false
    });
});