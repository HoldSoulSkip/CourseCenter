$(function () {

    //进行完成点击选中的代码
    var name = window.location.pathname;
   
    var ddaystrs = new Array();
    ddaystrs = name.split("/");
    var css = ddaystrs[2];

    if (css.indexOf("CourseIndex") > -1) {
        $("#CourseIndex").addClass("active");
    } else if (css.indexOf("StudentCenter") > -1) {
        $("#StudentCenter").addClass("active");
    }
    else if (css.indexOf("ModuleView") > -1) {
        $("#CourseIndex").addClass("active");
    }
    else if (css.indexOf("StudentCoursesDetail") > -1) {
        $("#CourseIndex").addClass("active");
    }
    else if (css.indexOf("StudentEnterCourse") > -1) {
        $("#CourseIndex").addClass("active");
    }
    else if (css.indexOf("GroupIndex") > -1) {
        $("#GroupIndex").addClass("active");
    }
    else if (css.indexOf("StudentGetGroupDetail") > -1) {
        $("#StudentIndex").addClass("active");
    }
    else if (css.indexOf("StudentGetMoreGroup") > -1) {
        $("#StudentIndex").addClass("active");
    }
    else if (css.indexOf("GroupDetail") > -1) {
        $("#GroupIndex").addClass("active");
    }
    else if (css.indexOf("QusetionCenter") > -1) {
        $("#QusetionCenter").addClass("active");
    }
    else if (css.indexOf("ScoreIndex") > -1) {
        $("#ScoreIndex").addClass("active");
    }
    else if (css.indexOf("StudentIndex") > -1) {
        $("#StudentIndex").addClass("active");
    } else if (css.indexOf("CoursesAllExcSelected") > -1) {
        $("#CourseIndex").addClass("active");
    }
    else if (css.indexOf("ShowAllBolgList") > -1) {
        $("#ShowAllBolgList").addClass("active");
    }
    else if (css.indexOf("AddNewBlog") > -1) {
        $("#ShowAllBolgList").addClass("active");
    } else if (css.indexOf("StudentGetBlogDetail") > -1) {
        $("#StudentIndex").addClass("active");
    }
    else if (css.indexOf("StudentGetMoreHotBlog") > -1) {
        $("#StudentIndex").addClass("active");
    }
   
    else if (css.indexOf("BlogDetail") > -1) {
        $("#ShowAllBolgList").addClass("active");
    } 
   




    // Side Bar Toggle
    $('.hide-sidebar').click(function () {
        $('#sidebar').hide('fast', function () {
            $('#content').removeClass('span9');
            $('#content').addClass('span12');
            $('.hide-sidebar').hide();
            $('.show-sidebar').show();
        });
    });


    $('.show-sidebar').click(function () {
        $('#content').removeClass('span12');
        $('#content').addClass('span9');
        $('.show-sidebar').hide();
        $('.hide-sidebar').show();
        $('#sidebar').show('fast');
    });
});