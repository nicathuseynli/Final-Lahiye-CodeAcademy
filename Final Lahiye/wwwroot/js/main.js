var lowerSlider = document.querySelector("#lower");
var upperSlider = document.querySelector("#upper");

document.querySelector("#two").value = upperSlider.value;
document.querySelector("#one").value = lowerSlider.value;

var lowerVal = parseInt(lowerSlider.value);
var upperVal = parseInt(upperSlider.value);

upperSlider.oninput = function () {
  lowerVal = parseInt(lowerSlider.value);
  upperVal = parseInt(upperSlider.value);

  if (upperVal < lowerVal + 4) {
    lowerSlider.value = upperVal - 4;
    if (lowerVal == lowerSlider.min) {
      upperSlider.value = 4;
    }
  }
  document.querySelector("#two").value = this.value;
};
 
lowerSlider.oninput = function () {
  lowerVal = parseInt(lowerSlider.value);
  upperVal = parseInt(upperSlider.value);
  if (lowerVal > upperVal - 4) {
    upperSlider.value = lowerVal + 4;
    if (upperVal == upperSlider.max) {
      lowerSlider.value = parseInt(upperSlider.max) - 4;
    }
  }
  document.querySelector("#one").value = this.value;
};
// filter Price End

// Sidebar start
const filter = document.getElementById("sidebar-btn");
const sidebar = document.getElementById("sidebar");
const closesidebar = document.getElementById("closeSidebar");

filter.addEventListener("click", (e) => {
  sidebar.classList.add("visible");
});
closesidebar.addEventListener("click", (e) => {
  sidebar.classList.remove("visible");
});
// sidebar end

// Single Page Photo start
function sendimg(a) {
  document.getElementById("mainimg").src = a.src;
}

// Single Page end
//Tabs Start
const tabs = document.querySelectorAll('.tab_btn');
const all_content = document.querySelectorAll('.content');

tabs.forEach((tab ,index)=>{
   tab.addEventListener('click',()=>{
    tabs.forEach(tab=>{tab.classList.remove('active')});
    tab.classList.add('active');
   })

})

//Tabs End


//mobile menu start
function toogleMobileMenu(menu){
  menu.classList.toogle('open');

}
//mobile menu end