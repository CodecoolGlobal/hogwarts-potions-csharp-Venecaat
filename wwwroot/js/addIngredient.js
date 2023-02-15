const brewingPotionIngredients = document.getElementById("ingredients-container");
const students = document.getElementById("students");
const ingredients = document.getElementById("ingredients");
const submitButton = document.getElementById("submit");
submitButton.addEventListener("click", showBrewingPotionForStudent);


async function showBrewingPotionForStudent(e) {
    event.preventDefault();
    let potion = await getBrewingPotionByStudentId(+students.value);

    if (e != undefined) {
        let ingredient = {
            name: ingredients.options[ingredients.selectedIndex].text
        };
        potion = await addIngredientToBrewingPotion(potion.id, ingredient);
    }

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
        submitButton.disabled = false;
    }
    else {
        content += "<p class=\"ml-4 pt-2\">You don't have any brewing potion!</p>";
        submitButton.disabled = true;
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
    return await null;
}

async function addIngredientToBrewingPotion(potionId, payload) {
    "use strict";
    let response = await fetch(`https://localhost:44390/api/Potion/${potionId}/add`, {
        method: "PUT",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(payload)
    });
    if (response.ok) {
        return await response.json();
    }
}