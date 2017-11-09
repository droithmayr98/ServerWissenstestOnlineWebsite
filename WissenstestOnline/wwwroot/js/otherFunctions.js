function changeSlider(input, rightVal) {
    console.log('enter changeSlider');
    var textfield = document.getElementById('textfield_slider');
    console.log("Slider Val: " + input);
    textfield.value = input;

    if (input == rightVal) {
        textfield.style.backgroundColor = "green";
    } else {
        textfield.style.backgroundColor = "red";
    }

}

function changeTextField(input, rightVal) {
    console.log('enter changeTextField');
    var textfield = document.getElementById('textfield_slider');
    var slider = document.getElementById('sliderBar');
    slider.value = input;

    if (input == rightVal) {
        textfield.style.backgroundColor = "green";
    } else {
        textfield.style.backgroundColor = "red";
    }
    

}