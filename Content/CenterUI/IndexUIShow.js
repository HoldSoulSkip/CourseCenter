/////控制首页模版显示的ui的Js
/***扩展方法--获取url地址中的参数**/

(function($){
    
    $.getUrlParam = function(name)
        
        {
            
            var reg = new RegExp("(^|&)"+ name +"=([^&]*)(&|$)");
            
            var r = window.location.search.substr(1).match(reg);
            
            if (r!=null) return unescape(r[2]); return null;
           
        }
        
    })(jQuery);


$(function () {
   
   var flag=  $.getUrlParam("flag");
 
   
    if(flag=="m"){
        $("#most").addClass("huang");
    }if(flag=="g"){
        $("#good").addClass("huang");
    } if (flag == "c") {
        $("#comments").addClass("huang");
    } if (flag == "p") {
        $("#push").addClass("huang");
    }

})




        
    




 