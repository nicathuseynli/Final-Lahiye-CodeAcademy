//-------------------------- Eye start
// Eye Icon start
const icon = document.querySelector(".icon")
const confirm_icon = document.querySelector(".conf_icon");
// Eye Icon End
// ========================
const from = document.getElementById("form");
const email = document.getElementById("email");
const username = document.getElementById("username");
const password = document.getElementById("password");
const confirm_password = document.getElementById("confirm_password");

icon.addEventListener("click", () => {
  if (password.getAttribute("type") === "password") {
    password.setAttribute("type", "text");
    icon.classList.replace("fa-eye-slash", "fa-eye");
  } else {
    password.setAttribute("type", "password");
    icon.classList.replace("fa-eye", "fa-eye-slash");
  }
});

confirm_icon.addEventListener("click", () => {
  if (confirm_password.getAttribute("type") === "password") {
    confirm_password.setAttribute("type", "text");
    confirm_icon.classList.replace("fa-eye-slash", "fa-eye");
  } else {
    confirm_password.setAttribute("type", "password");
    confirm_icon.classList.replace("fa-eye", "fa-eye-slash");
  }
});
//------------------------------------ Eye End