document.addEventListener('DOMContentLoaded', function () {
    projectCarousel();
})
window.onload = function () {
    projectCarousel();
}

window.addEventListener("resize", function () {
    projectCarousel();
})

function projectCarousel() {
    const projectContainer = document.querySelector(".element-project-grid-container");
    const project = document.querySelector(".grid-box-wrapper");
    const slides = document.querySelectorAll(".grid-box-wrapper");
    const nextSlide = document.querySelector(".next-slide");
    const prevSlide = document.querySelector(".prev-slide");
    let windowWidth = window.innerWidth

    if (windowWidth < 768 && slides.length > 0) {
        //console.log("if")
        let projectHeight = parseFloat(window.getComputedStyle(project).height);
        projectContainer.style.height = projectHeight + "px";
        let i = 0;
        let currentSlide = slides[i];

        HideSlides(slides, currentSlide);

        nextSlide.addEventListener("click", function (e) {
            e.preventDefault();
            //console.log("next")
            //console.log(i)
            if (i >= Math.ceil(slides.length - 1)) {
                i = 0
                currentSlide = slides[i]
            }
            else {
                i = i + 1
                currentSlide = slides[i];
            }
            //console.log(slides)
            HideSlides(slides, currentSlide);
        })

        prevSlide.addEventListener("click", function (e) {
            e.preventDefault();
            //console.log("prev")
            //console.log(i)
            if (i <= 0) {
                i = slides.length - 1
                currentSlide = slides[i]
            }
            else {
                i = i - 1;
                currentSlide = slides[i];
            }
            //console.log(slides)
            HideSlides(slides, currentSlide);
        })
    }
    else {
        //console.log("else")
        projectContainer.style.height = "inherit";
        slides.forEach(slide => {
            if (slide.classList.contains("hide-project")) {
                slide.classList.remove("hide-project")
            }
        })
    }
}

function HideSlides(slides, currentSlide) {
    slides.forEach(slide => {
        if (slide === currentSlide) {
            if (slide.classList.contains("hide-project")) {
                slide.classList.remove("hide-project")
                slide.classList.add("active-project")
            }
        }
        else {
            if (slide.classList.contains("active-project")) {
                slide.classList.remove("active-project")
            }
            slide.classList.add("hide-project")
        }
        //console.log(slide)
    })
}