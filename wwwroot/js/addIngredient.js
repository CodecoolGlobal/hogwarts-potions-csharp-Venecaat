const brewingPotionIngredients = document.getElementById("ingredients-container");
const students = document.getElementById("students");
const ingredients = document.getElementById("ingredients");


async function showBrewingPotionForStudent() {
    let potion = await getBrewingPotionByStudentId(+students.value);
    console.log(potion);

    let content = "";

    if (potion != null) {
        if (potion.ingredients.length === 0) {
            content += '<p class="ml-4 pt-2">No ingredients yet!</p>';
        }
        else {
            content += "<ul>";
            for (let ing of potion.ingredients) {
                content += `<li class="mt-3">${ing.name}</li>`;
            }
            content += "</ul>";
        }
    }
    else {
        content += "<p class=\"ml-4 pt-2\">You don't have any brewing potion!</p>";
    }
    brewingPotionIngredients.innerHTML = content;
}

async function getBrewingPotionByStudentId(id) {
    "use strict";
    let response = await fetch(`https://localhost:44390/api/Potion/brewing/${id}`, {
        method: "GET"
    });
    if (response.status === 200) {
        return await response.json();
    }
    else if (response.status === 204) {
        return await null;
    }
}