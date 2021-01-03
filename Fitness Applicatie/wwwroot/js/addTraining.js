function addExerciseField() {
    var setGroup = document.getElementById("setGroup");
    var exerciseGroup = document.getElementById("exerciseGroup");
    var trainingForm = document.getElementById("trainingForm");
    var addSetButton = document.getElementById("addSetButton");
    var nameLbl = document.getElementById("nameLbl");
    var ExerciseName = document.getElementById("Execise.Name");
    var setsLBl = document.getElementById("setsLbl");
    var Sets = document.getElementById("Sets");

    var exerciseGroupClone = exerciseGroup.cloneNode();
    var setGroupClone = setGroup.cloneNode();
    var addSetButtonClone = addSetButton.cloneNode(true);
    addSetButtonClone.id = "addSetButton" + nextExerciseGroup();
    var nameLBlClone = nameLbl.cloneNode(true);
    var ExerciseNameClone = ExerciseName.cloneNode();
    var setsLblClone = setsLBl.cloneNode(true);
    var SetsClone = Sets.cloneNode();
    var breakline1 = document.createElement("br");
    var breakline2 = document.createElement("br");
    var breakline3 = document.createElement("br");


    setGroupClone.appendChild(nameLBlClone);
    setGroupClone.appendChild(ExerciseNameClone);
    setGroupClone.appendChild(setsLblClone);
    setGroupClone.appendChild(SetsClone);
    setGroupClone.appendChild(addSetButtonClone);
    setGroupClone.appendChild(breakline1);
    exerciseGroupClone.appendChild(setGroupClone);
    trainingForm.appendChild(exerciseGroupClone);
    trainingForm.appendChild(breakline2);
    trainingForm.appendChild(breakline3);
}

function nextExerciseGroup() {
    if (typeof nextExerciseGroup.counter == 'undefined') {
        nextExerciseGroup.counter = 0;
    }

    nextExerciseGroup.counter++;
    return nextExerciseGroup.counter;
}

function addSetField(button_id) {
    var addSetButton = document.getElementById(button_id);
    var setGroup = addSetButton.parentElement;
    var exerciseGroup = setGroup.parentElement;
    var setsLbl = document.getElementById("setsLbl");
    var Sets = document.getElementById("Sets");

    var setGroupClone = setGroup.cloneNode();
    var setsLblClone = setsLbl.cloneNode(true);
    var SetsClone = Sets.cloneNode(true);
    var addSetButtonClone = addSetButton.cloneNode(true);
    var breakline = document.createElement("br");

    addSetButton.remove();
    exerciseGroup.appendChild(setGroupClone);
    setGroupClone.appendChild(setsLblClone);
    setGroupClone.appendChild(SetsClone);
    setGroupClone.appendChild(addSetButtonClone);
    setGroupClone.appendChild(breakline);
}