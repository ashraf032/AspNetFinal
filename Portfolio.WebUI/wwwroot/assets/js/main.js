jQuery( document ).ready(function( $ ) {
"use strict"
/*-----------------------------------------------------------------------------------*/
/*    PORTFOLIO FILTER
/*-----------------------------------------------------------------------------------*/
    $('#Container').mixItUp();


    let span = document.querySelectorAll(".clipboard")
    span.forEach(element => {
        element.addEventListener("click", function (e) {
            e.preventDefault();

            let href = e.currentTarget.getAttribute("href")
            navigator.clipboard.writeText(href);


        })
    })
    
});





