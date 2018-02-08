
var btnAdd = document.getElementById('add');

var btnRemove = document.getElementById('remove');

var AddPeopleList = document.getElementById('ListId');

var ToAddList = document.getElementById('Inbjudna');


btnAdd.addEventListener("click", function () {
    var ListLength = AddPeopleList.options.length;
    console.log(ListLength);
    for (var i = 0; i < ListLength; i++) {
        console.log(AddPeopleList.options[i].selected);
        if (AddPeopleList.options[i].selected) {
            ToAddList.add(new Option(AddPeopleList.options[i].text));
            AddPeopleList.remove(i);
        }
    }
});

btnRemove.addEventListener("click", function () {
    var ListLength = ToAddList.options.length;
    console.log(ListLength);
    for (var i = 0; i < ListLength; i++) {
        console.log(ToAddList.options[i].selected);
        if (ToAddList.options[i].selected) {
            AddPeopleList.add(new Option(ToAddList.options[i].text));
            ToAddList.remove(i);
        }
    }
});

