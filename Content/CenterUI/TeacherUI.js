$(function () {

    //进行完成点击选中的代码
    var name = window.location.pathname;
    var ddaystrs = new Array();
    ddaystrs = name.split("/");

    var css = ddaystrs[1];
    if (css.indexOf("CourseManage") > -1) {
        $("#CourseManage").addClass("active");
    } else if (css.indexOf("LearnerManage") > -1) {
        $("#LearnerManage").addClass("active");
    } else if (css.indexOf("Home") > -1) {
        $("#Home").addClass("active");
    }


})