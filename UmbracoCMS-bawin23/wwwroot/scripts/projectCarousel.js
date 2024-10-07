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
    const projectContainer = document.querySelector(".project-carousel");
    const project = document.querySelector(".carousel-item").firstElementChild;
    const slides = projectContainer.querySelectorAll(".carousel-item");
    const nextSlide = projectContainer.querySelector(".next-slide");
    const prevSlide = projectContainer.querySelector(".prev-slide");
    let windowWidth = window.innerWidth

    if (windowWidth > 767 || slides.length <= 0) {
        projectContainer.style.height = "inherit";
        slides.forEach(slide => {
            if (slide.classList.contains("hide-project")) {
                slide.classList.remove("hide-project")
            }
        })
    }
    else {
        let projectHeight = parseFloat(window.getComputedStyle(project).height);
        projectContainer.style.height = projectHeight + "px";
        let cardIndex = 0;
        let currentSlide = slides[cardIndex];

        HideSlides(slides, currentSlide);

        nextSlide.addEventListener("click", function (e) {
            e.preventDefault();
            if (cardIndex >= Math.ceil(slides.length - 1)) {
                cardIndex = 0
                currentSlide = slides[cardIndex]
            }
            else {
                cardIndex = cardIndex + 1
                currentSlide = slides[cardIndex];
            }
            HideSlides(slides, currentSlide);
        })

        prevSlide.addEventListener("click", function (e) {
            e.preventDefault();
            if (cardIndex <= 0) {
                cardIndex = slides.length - 1
                currentSlide = slides[cardIndex]
            }
            else {
                cardIndex = cardIndex - 1;
                currentSlide = slides[cardIndex];
            }
            HideSlides(slides, currentSlide);
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
    })
}