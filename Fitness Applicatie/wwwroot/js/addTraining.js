function addExerciseField() {
    var setGroup = document.getElementById("setGroup");
    var addSetButtonFormat = document.getElementById("addSetButtonFormat");
    var exerciseGroup = document.getElementById("exerciseGroup");
    var trainingForm = document.getElementById("trainingForm");
    var nameLbl = document.getElementById("nameLbl");
    var ExerciseName = document.getElementById("Rounds_0__Exercise_Name");
    var setsLBl = document.getElementById("setsLbl");
    var Sets = document.getElementById("Rounds_0__Sets_0__Weight");
    var nextExerciseGroupNumber = nextExerciseGroup();
    var addTrainingSubmit = document.getElementById("addTrainingSubmit");


    //Rounds_0__Exercise_Name
    //Rounds[0].Exercise.Name

    var exerciseGroupClone = exerciseGroup.cloneNode();
    var setGroupClone = setGroup.cloneNode();
    var addSet = addSetButtonFormat.cloneNode(true);
    addSet.removeAttribute("hidden");
    addSet.id = "addSetButton@" + nextExerciseGroupNumber + "_0";
    var nameLBlClone = nameLbl.cloneNode(true);
    var ExerciseNameClone = ExerciseName.cloneNode();
    ExerciseNameClone.id = "Rounds_" + nextExerciseGroupNumber + "__Exercise_Name";
    ExerciseNameClone.name = "Rounds[" + nextExerciseGroupNumber + "].Exercise.Name";
    var setsLblClone = setsLBl.cloneNode(true);
    var SetsClone = Sets.cloneNode();
    SetsClone.id = "Rounds_" + nextExerciseGroupNumber + "__Sets_0__Weight";
    SetsClone.name = "Rounds[" + nextExerciseGroupNumber + "].Sets[0].Name";
    var breakline1 = document.createElement("br");
    var breakline2 = document.createElement("br");
    var breakline3 = document.createElement("br");


    setGroupClone.appendChild(nameLBlClone);
    setGroupClone.appendChild(ExerciseNameClone);
    setGroupClone.appendChild(setsLblClone);
    setGroupClone.appendChild(SetsClone);
    setGroupClone.appendChild(addSet);
    setGroupClone.appendChild(breakline1);
    exerciseGroupClone.appendChild(setGroupClone);
    trainingForm.appendChild(exerciseGroupClone);
    trainingForm.appendChild(breakline2);
    trainingForm.appendChild(breakline3);
    trainingForm.appendChild(addTrainingSubmit);
}

function nextExerciseGroup() {
    if (typeof nextExerciseGroup.counter == 'undefined') {
        nextExerciseGroup.counter = 0;
    }

    nextExerciseGroup.counter++;
    return nextExerciseGroup.counter;
}

    //Rounds[0].Sets[0].Weight
    //Rounds_0__Sets_0__Weight

function addSetField(button_id) {
    var indexOfRN = button_id.indexOf("@");
    var indexofSN = button_id.indexOf("_");
    var exerciseGroupNumber = button_id.substring(indexOfRN + 1, indexofSN);
    var setNumber = parseInt(button_id.substring(indexofSN + 1)) + 1;
    if (exerciseGroupNumber == "") {
        exerciseGroupNumber = 0;
    }
    var addSetButton = document.getElementById(button_id);
    var setGroup = addSetButton.parentElement;
    var exerciseGroup = setGroup.parentElement;
    var setsLbl = document.getElementById("setsLbl");
    var Sets = document.getElementById("Rounds_0__Sets_0__Weight");

    var setGroupClone = setGroup.cloneNode();
    var setsLblClone = setsLbl.cloneNode(true);
    var SetsClone = Sets.cloneNode(true);
    SetsClone.id = "Rounds_" + exerciseGroupNumber + "__Sets_" + setNumber + "__Weight";
    SetsClone.name = "Rounds[" + exerciseGroupNumber + "].Sets[" + setNumber + "].Weight";
    var addSetButtonClone = addSetButton.cloneNode(true);
    addSetButtonClone.id = "addSetButton@" + exerciseGroupNumber + "_" + setNumber;
    var breakline = document.createElement("br");

    addSetButton.remove();
    exerciseGroup.appendChild(setGroupClone);
    setGroupClone.appendChild(setsLblClone);
    setGroupClone.appendChild(SetsClone);
    setGroupClone.appendChild(addSetButtonClone);
    setGroupClone.appendChild(breakline);
}