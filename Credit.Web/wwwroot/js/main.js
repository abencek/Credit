$( document ).ready(function() {
    "use strict";

    //Navigation toggle event handler
    $("#toggle-button").on('click', function(e) {
        e.preventDefault();
        $("body").toggleClass("sidebar-toggled");
        $(".sidebar").toggleClass("toggled"); 
        $("#toggle-button div").toggleClass("toggle-on");
    });

    //Filter event handler for Agreement and Customer view
    $("#filter").on("submit",function(e){
        //Disable editing in empty filter fields before form submission
        $(this).find(":input").each(function(){
            if(!$(this).val()){$(this).attr("disabled","true")};
        });
    });

    //Event handler for modal dialog in Agreement and Customer view
    $("#data-table tbody td a[target=_self]").on('click', function(e) {
      e.preventDefault();

      const modalDialog = $("#modal-window");
      const sendUrl=$(this).attr('href');
      const selectedId = sendUrl.substring(sendUrl.lastIndexOf("/")+1);
        
      const updateTable = function(data) {
        let row;
        //Find row in table
        $("#data-table tbody tr").find('td:first').each(function(){
            if($(this).text() == selectedId){
            row = $(this).parent().children().not(":last");
            return false;
            }
        });
        //Fade animation on row update
        $(row).fadeOut("fast").promise().done(function(){
            $(this).each(function(index,element){
                $(element).text(data[index])
            });
            $(row).fadeIn(200); 
        });
      }

      const submitModalWindow = function() {
        const submitButton=$(this);
        const sendData = decodeURIComponent(modalDialog.find("form").serialize());
        $.ajax({
          url: sendUrl,
          type:'POST',
          contentType: 'application/x-www-form-urlencoded; charset=UTF-8', 
          dataType:'json',
          data: sendData,
          success: function(receivedData){  
            modalDialog.modal('hide');
            submitButton.off('click');
            if(receivedData.length>0) {
              updateTable(receivedData);
            }
          }
        });
      }

      //Create and display modal window
      $.ajax({
        url: sendUrl,
        type:'GET',   
          success: function (data) {
                //Set modal window title
                modalDialog.find("#modal-title span").first()
                    .text(data[modalDialog.find("input[type=hidden]").first().attr("name")]);
                //Set modal window fields
                modalDialog.find("form :input").each(function(){
                    var name=$(this).attr('name');
                    $(this).val(data[name]);
                })
                //Set modal window submit button
                modalDialog.find("#modal-button-submit")
                .off('click')
                .on('click', function (e) {
                    e.preventDefault();
                    submitModalWindow();
                });
                //Display modal window
                modalDialog.modal('show');
          }
      });
  });
}); 
  

