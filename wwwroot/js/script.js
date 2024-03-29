const ul = document.querySelector("tagsList"),
input = document.querySelector("input"),
tagNumb = document.querySelector(".details span");

let maxTags = 5,
tags = ["C#", "JS"];

countTags();
createTag();

function countTags(){
    input.focus();
    tagNumb.innerText = maxTags - tags.length;
}

function createTag(){
    tagsList.querySelectorAll("li").forEach(li => li.remove());
    tags.slice().reverse().forEach(tag =>{
        let liTag = `<li>${tag} <i class="uit uit-multiply" onclick="remove(this, '${tag}')"></i></li>`;
        tagsList.insertAdjacentHTML("afterbegin", liTag);
    });
    countTags();
}

function remove(element, tag){
    let index  = tags.indexOf(tag);
    tags = [...tags.slice(0, index), ...tags.slice(index + 1)];
    element.parentElement.remove();
    countTags();
}

function addTag(e){
    if(e.key == "Enter"){
        let tag = e.target.value.replace(/\s+/g, ' ');
        if(tag.length > 1 && !tags.includes(tag)){
            if(tags.length < 5){
                tag.split(',').forEach(tag => {
                    tags.push(tag);
                    createTag();
                });
            }
        }
        e.target.value = "";
    }
}

input.addEventListener("keyup", addTag);

const removeAllButton = document.querySelector(".RemoveAllButton");

removeAllButton.addEventListener("click", () => {
    tags.length = 0; // Clears the tags array
    tagsList.querySelectorAll("li").forEach(li => li.remove()); 
    countTags(); // Updates the tag count
});