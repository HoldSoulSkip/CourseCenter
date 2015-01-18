
/*
	*名称: caids.js
	*日期: 2012-6-16
	*功能: 影楼/企业网站动态效果页面
	*作者:明天以后
	*版权: 魔方互动科技有限公司
 */

$(function()
		   {$(".id_case_nr").scrollable({size:4,items:".b2 ul",loop:true,prev:".l2",next:".r2"}).autoscroll({autoplay:true,autopause:true,interval:2000});
		   $(".b2").scrollable({size:4,items:".b2 ul",loop:true,prev:".l2",next:".r2"}).autoscroll({autoplay:true,autopause:true,interval:2000});

});


// JavaScript DocumentjQuery(function(){
jQuery(document).ready(function() {
//jQuery("a").focus(function(){jQuery(this).blur();});					   
//jQuery("a").focus(function(){jQuery(this).blur();});		


if(jQuery("ul.none li").size()>0)
{
jQuery("ul.none").tabs(".demo > div", {effect: 'fade',loop:true,fadeOutSpeed: "fast",rotate: true}).slideshow({autoplay:true,interval: 3000});
}
});


