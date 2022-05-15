$(document).ready(function () {

  //Settings for anime.js library
  anime.set('.anime-group-1, .anime-group-2, .anime-group-3', {opacity:0});

  var tline = anime.timeline({easing: 'easeOutExpo'});
    
  tline
    .add({
      targets: '.anime-group-1',
      opacity: 1,
      duration: 500
    })
    .add({
      targets: '.anime-group-2',
      scaleY: [0,1],
      opacity: 1,
      delay: anime.stagger(100),
      duration:500
    })
    .add({
        targets: '.anime-group-3',
        scaleY: [0,1],
        opacity: 1,
        easing: 'easeOutElastic',
        duration: 400
    });

});
