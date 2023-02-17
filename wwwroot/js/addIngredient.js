const brewingPotionIngredients = document.getElementById("ingredients-container");
const students = document.getElementById("students");
const ingredients = document.getElementById("ingredients");
const submitButton = document.getElementById("submit");
const helpButton = document.getElementById("help");
const form = document.querySelector("form");
const recipesList = document.getElementById("recipes-list");

submitButton.addEventListener("click", showBrewingPotionForStudent);
helpButton.addEventListener("click", showRecipes);


async function showBrewingPotionForStudent(e) {
    event.preventDefault();

    let potion = await getBrewingPotionByStudentId(+students.value);

    if (e != undefined) {
        let ingredient = {
            name: ingredients.options[ingredients.selectedIndex].text
        };
        if (potion.ingredients.length < 4) {
            potion = await addIngredientToBrewingPotion(potion.id, ingredient);

            if (recipesList.innerHTML !== "") showRecipes();
        }
    }
    else {
        recipesList.innerHTML = "";
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
        if (potion.ingredients.length === 4) {
            form.setAttribute("asp-action", "FinalizePotion");
            submitButton.removeEventListener("click", showBrewingPotionForStudent);
        }
    }
    else {
        content += "<p class=\"ml-4 pt-2\">You don't have any brewing potion!</p>";
        submitButton.disabled = true;
    }

    if (potion == null || potion.ingredients.length === 0) {
        helpButton.disabled = true;
    }
    else {
        helpButton.disabled = false;
    }

    brewingPotionIngredients.innerHTML = content;
}

async function showRecipes() {
    const potion = await getBrewingPotionByStudentId(+students.value);
    let errorMsg = "";
    const recipes = await getPossibleRecipes(potion.id).catch(error => { errorMsg = error; });
    
    let content = "";

    if (recipes == undefined) content += `<p class="font-weight-bold">${errorMsg.message}</p >`;
    else
    {
        for (let recipe of recipes) {
            content += "<p>";
            content += `<span class="font-weight-bold">${recipe.name}</span> - `;
            for (let ingredient of recipe.ingredients) {
                content += `${ingredient.name}, `;
            }
            content += "</p >";
        }
    }

    recipesList.innerHTML = content;
}

// API Requests
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

async function getPossibleRecipes(potionId) {
    "use strict";
    let response = await fetch(`https://localhost:44390/api/Potion/${potionId}/help`, {
        method: "GET"
    });
    if (response.status === 200) {
        return await response.json();
    }
    return response.text().then(text => {throw new Error(text)});
}
