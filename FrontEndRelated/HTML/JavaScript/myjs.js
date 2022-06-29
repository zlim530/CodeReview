let btnList = document.querySelectorAll(".numberList button");
let imgContainer = document.querySelector(".imgContainer");
for (let i in btnList) {
  btnList[i].onclick = function () {
    // 0 * -640 = 0;
    // 1 * -640 = -640;
    // 2 * -640 = -1280;
    // 形变-平移
    imgContainer.style.transform = `translate(${-640 * i}px)`;
  };
}