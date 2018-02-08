//Load product group on page load
$(document).ready(function ()
{
    var Ulgroup = $('#groupsidebar');
    $.ajax({
        type: 'get',
        url: 'api/ProductGroups',
        datatype: 'json',
        success: function (data) {
            Ulgroup.empty();
            $.each(data, function (index,value) {
                Ulgroup.append('<li class="groupname" ><a>' + value.groupName + '</a></li>');
               // alert(data.groupName);
            });
        }
    });
});


//Load Products on group click
