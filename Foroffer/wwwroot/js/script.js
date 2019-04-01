
let a = document.getElementById("mobbtn");
a.onclick = function Show(){
    let b =  document.getElementById("catblock");
    if(b.style.display == "block")
    {
        b.style.display = "none";
    }
    else
    {
        b.style.display = "block";
    }
}

let c = document.getElementById("minimenu");
c.onclick = function Mobshow(){
    let d =  document.getElementById("catblock");
    if(d.style.display == "block")
    {
        d.style.display = "none";
    }
    else
    {
        d.style.display = "block";
    }
}



/*   function changeClass(){
   let a = document.getElementsByClassName("lastinfo");
   let width = window.innerWidth;
    if(width <= 505){
        for(var i=0; i < a.length; i++){
            a[i].className = "mobinfo";
        }
     }
   } */


