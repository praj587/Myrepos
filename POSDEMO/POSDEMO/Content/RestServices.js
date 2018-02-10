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
                Ulgroup.append('<li class="groupname"  id="' + value.groupId + '" ><a>' + value.groupName + '</a></li>');
               // alert(data.groupName);
            });
        }
    });
});
//Load all products on page load
$(document).ready(function () {
    var Ulgroup = $('#prods');
    $.ajax({
        type: 'get',
        url: 'api/Products',
        datatype: 'json',
        success: function (data) {
            Ulgroup.empty();
            $.each(data, function (index, value) {
                Ulgroup.append('<li class="clearfix products" id="' + value.productId + '" ><img class="round" src="' + value.productimgUrl + '"><div class="legend-info"><strong class="pname">' + value.productName + '</strong><span class="pcurrency"> ₹</span><span class="pprice">' + value.productPrice + '</span></div></li>');
                // alert(data.groupName);
            });
        }
    });
});


//Load Products on group click
//$('#groupname').click(function () {
$(document.body).on('click', '.groupname', function (e) {
    var groupid;
    var Ulgroup = $('#prods');
    groupid = $(this).attr("id");
    $('.lblproductheader').text($(this).text());
    $.ajax({
        type: 'get',
        url: 'api/Products/id',
        datatype: 'json',
        success: function (data) {
            Ulgroup.empty();
            $.each(data, function (index, value) {
                Ulgroup.append('<li class="clearfix products" id="' + value.productId + '" ><img class="round" src="' + value.productimgUrl + '"><div class="legend-info"><strong class="pname">' + value.productName + '</strong><span class="pcurrency"> ₹</span><span class="pprice">' + value.productPrice + '</span></div></li>');
                // alert(data.groupName);
            });
        }
    });
});