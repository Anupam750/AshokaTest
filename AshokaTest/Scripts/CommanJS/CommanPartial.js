function ValidateRules(obj) {
    $('#lblCourseMessage').val("");
    var CheckedItems = $('input[type="checkbox"]:checked').length;
    var minCourse = $("#Min").val();
    var maxCourse = $("#Max").val();

    if (minCourse === maxCourse) {
        if (CheckedItems > maxCourse) {
            $(obj).prop('checked', false); // Unchecks it
            alert("sorry, You can choose up to " + maxCourse + "");
        }
        else {
            if (minCourse > CheckedItems) {
                // $(obj).prop('checked', false); // Unchecks it
                // alert("sorry, You need choose atleast " + minCourse + "");
            }
        }
    }
    else {

        if (CheckedItems > maxCourse) {
            $(obj).prop('checked', false); // Unchecks it
            alert("sorry, You can choose up to " + maxCourse + "");
        }
        if (CheckedItems < minCourse) {
            $(obj).prop('checked', false); // Unchecks it
            alert("sorry, You need choose atleast " + minCourse + "");
        }
    }

    var abc = "";
    //Iterating the collection of checkboxes which checked marked
    $('input[type=checkbox]').each(function () {
        if (this.checked) {
            abc = abc + $(this).val() + ","
            //assign set value to hidden field
            $('#CourseIDs').val(abc);
            $('#CoursesID').val(abc);
        }
    });
}
  


