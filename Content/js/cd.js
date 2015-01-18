// JavaScript Document$(function(){
$(document).ready(function() {
/*$("a").focus(function(){$(this).blur();});					   
$("a").focus(function(){$(this).blur();});*/		
$("a.np1,a.np2").click(function(){return false});

$(".contenter_caseinbom").scrollable({size:1,items:".contenter_casejs ul",loop:true}).autoscroll({ autoplay:true,interval:4000}).navigator({navi:"ul.navi",naviItem:"li",activeClass:"current"});

$("ul.nav li.n1").hover(function(){
								 
								 
				$(this).find("i").stop().animate({top:"-32px"});
				
				$(this).find("span").stop().animate({top:"0px"});
				
				$(this).find("ul").stop(true,true).slideDown("123");
				
								 
								 },function(){
									 
				$(this).find("i").stop().animate({top:"0px"});
				
				$(this).find("span").stop().animate({top:"32px"});		
				
				$(this).find("ul").stop(true,true).slideUp("123");
									 
									 })
});
$(function(){$(".b2").scrollable({size:4,items:".b2 ul",loop:true,prev:".l2",next:".r2"}).autoscroll({autoplay:true,autopause:true,interval:2000});
});
