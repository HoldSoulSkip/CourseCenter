$(function () {

    //进行完成点击选中的代码
    var name = window.location.pathname;
    var ddaystrs = new Array();
    ddaystrs = name.split("/");
   
    var css = ddaystrs[1];
    
    if (css.indexOf("CourseManage") > -1) {
        $("#CourseManage").addClass("active");

    } else if (css.indexOf("ModuleManage") > -1) {
        $("#CourseManage").addClass("active");
    }
    else if (css.indexOf("LearnerManage") > -1) {
        $("#LearnerManage").addClass("active");
    } else if (css.indexOf("Home") > -1) {
        $("#Home").addClass("active");

    }
  
    else if (css.indexOf("PersonalManage") > -1) {
        $("#GetPersonalInfo").addClass("active");
    }
    else if (css.indexOf("QusetionManage") > -1) {
        $("#QusetionManage").addClass("active");
    }
    else if (css.indexOf("LoadMore") > -1) {
        $("#Home").addClass("active");
    }






    // Side Bar Toggle
    $('.hide-sidebar').click(function() {
	  $('#sidebar').hide('fast', function() {
	  	$('#content').removeClass('span9');
	  	$('#content').addClass('span12');
	  	$('.hide-sidebar').hide();
	  	$('.show-sidebar').show();
	  });
    });
    

	$('.show-sidebar').click(function() {
		$('#content').removeClass('span12');
	   	$('#content').addClass('span9');
	   	$('.show-sidebar').hide();
	   	$('.hide-sidebar').show();
	  	$('#sidebar').show('fast');
	});
});