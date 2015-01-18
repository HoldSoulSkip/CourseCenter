$(function () {

    //进行完成点击选中的代码
    var name = window.location.pathname;
   
    var ddaystrs = new Array();
    ddaystrs = name.split("/");
    var css = ddaystrs[2];
   
    if (css.indexOf("AdminCoursesDetail") > -1) {
        $("#AdminCourseView").addClass("active");
    } else if (css.indexOf("AdminCoureseView") > -1) {
        $("#AdminCourseView").addClass("active");
    } else if (css.indexOf("AdminIndex") > -1) {
        $("#Home").addClass("active");
    } else if (css.indexOf("AdminGetMoreBlog") >-1|| css.indexOf("AdminGetBlogDetail") > -1) {
        $("#Home").addClass("active");
    }
    else if (css.indexOf("AdminGetGroupDetail") > -1 || css.indexOf("AdminGetMoreGroup") > -1) {
        $("#Home").addClass("active");
    }

    else if (css.indexOf("AdminLearnesView") > -1) {
        $("#AdminLearnesView").addClass("active");
    }
    else if (css.indexOf("AdminTeacherView") > -1) {
        $("#AdminTeacherView").addClass("active");
    }
    else if (css.indexOf("AdminTeacherCoursesView") > -1) {
        $("#AdminTeacherView").addClass("active");
    } else if (css.indexOf("GetPersonalInfo") > -1) {
        $("#GetPersonalInfo").addClass("active");
    }
    else if (css.indexOf("ShowAllGroups") > -1) {
        $("#ShowAllGroups").addClass("active");
    }
    else if (css.indexOf("GroupDetail") > -1) {
        $("#ShowAllGroups").addClass("active");
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