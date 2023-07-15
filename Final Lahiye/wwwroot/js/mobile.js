function Nav() {
    var width = document.getElementById("mySidenav").style.width;
    if (width === "0px" || width == "") {
        document.getElementById("mySidenav").style.width = "250px";
        $('.animated-icon').toggleClass('open');
    }
    else {
        document.getElementById("mySidenav").style.width = "0px";
        $('.animated-icon').toggleClass('open');
    }
}

window.onscroll = function () {
    myFunction();
};

var mob = document.getElementById("mob");
var sticky = mob.offsetTop;

function myFunction() {
    if (window.pageYOffset >= sticky) {
        mob.classList.add("sticky");
    } else {
        mob.classList.remove("sticky");
    }
}