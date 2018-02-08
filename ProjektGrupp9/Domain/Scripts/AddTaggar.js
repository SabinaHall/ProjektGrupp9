var btnAddTag = document.getElementById('addTag');

var btnRemoveTag = document.getElementById('removeTag');

var AddTagList = document.getElementById('SelectedTags');

var ToAddTagList = document.getElementById('SelectedTagIds');


btnAddTag.addEventListener("click", function () {
    var ListLength = AddTagList.options.length;
    console.log(ListLength);
    for (var i = 0; i < ListLength; i++) {
        console.log(AddTagList.options[i].selected);
        if (AddTagList.options[i].selected) {
            ToAddTagList.add(new Option(AddTagList.options[i].text));
            AddTagList.remove(i);
        }
    }
});

btnRemoveTag.addEventListener("click", function () {
    var ListLength = ToAddTagList.options.length;
    console.log(ListLength);
    for (var i = 0; i < ListLength; i++) {
        console.log(ToAddTagList.options[i].selected);
        if (ToAddTagList.options[i].selected) {
            AddTagList.add(new Option(ToAddTagList.options[i].text));
            ToAddTagList.remove(i);
        }
    }
});

