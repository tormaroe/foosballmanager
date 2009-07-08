
function confirmAdminOperation(extraConfirmMessage)
{
    if(extraConfirmMessage != null && extraConfirmMessage.length > 0)
    {
        if(!confirm(extraConfirmMessage))
        {
            return false;
        }
    }
    
    var userPassword = prompt('Please provide admin password to confirm');
    
    if(userPassword == 'obifuss')
    {
        return true;
    }
    
    return false;
}