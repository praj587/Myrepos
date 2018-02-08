//On Product li click add row to summary
$("#prods li").click(function (e) {
    var prodname;
    var prodprice;
    var prodid;
    prodid = $(this).attr("id");
    prodname = $(this).find(".pname").text();
    prodprice = $(this).find(".pprice").text();
    var tf = true;
    tf = changeqty(prodid, prodprice, tf,"false");
    if (tf == true)
    {
        AddRow(prodid,prodname, 1, prodprice);
    }
    BillCalc();
});

//change quantity
function changeqty(prodid, prodprice, tf,minus)
{
    $("#tblsummary tr.item").each(function () {
        var p_qty = 0;
        var p_id = $(this).find(".iid").text();
        p_qty = parseInt($(this).find(".iqty").text());
        if (p_id == prodid) {

            if (minus == "false") {
                p_qty = p_qty + 1;
                $(this).find(".iqty").text(p_qty);
                $(this).find(".iamt").text(p_qty * parseFloat(prodprice));
            }
            else if (minus == "true") {
                
                p_qty = p_qty == 1 ? 0 : p_qty - 1;
                if (p_qty == 0) {
                    var r = confirm("Item will be removed.Do you want to continue?");
                }
                if (r == true) { p_qty = 0 } else { p_qty = 1 }
                if (p_qty == 0 ) {
                    $(this).remove();

                }
                else {
                    $(this).find(".iqty").text(p_qty);
                    $(this).find(".iamt").text(p_qty * parseFloat(prodprice));
                }
            }
            else {
                alert("invalid Operation");
                return false;
            }

            tf = false;
        }

    });
    return tf;
}
//Add new row to summary table
function AddRow(p_id,p_name, p_qty,p_price) {
    $('#tblsummary tbody').append('<tr class="item"><th scope="row"  class="hidden iid">' + p_id + '</th><td class="inegate"> <span class="glyphicon glyphicon-minus"></span></td><td class="iname">' + p_name + '</td> <td class="iprice">' + p_price + '</td><td class="iqty">' + 1 + '</td><td class="iamt">' + p_price + '</td></tr>');
};

//Calculate total bill amount from summry table
function BillCalc() {
    var B_tot = 0;
    $("#tblsummary tr.item").each(function () {
        B_tot = B_tot + parseFloat($(this).find(".iamt").text());
    });
    $(".lbltotal").text(B_tot);
}

//Negate the quantity when click minus  Credit:Churchill

$("#tblsummary").on("click", ".inegate", function () {
    var prodprice;
    var prodid;
    prodid = $(this).closest("tr").find(".iid").text();
    prodprice = $(this).closest("tr").find(".iprice").text();
    var tf = true;
    tf = changeqty(prodid, prodprice, tf, "true");
    
    BillCalc();
});