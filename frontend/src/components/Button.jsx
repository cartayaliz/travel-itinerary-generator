function Button({texto, onClick, type= "button"})
{
    return(
    <button type={type} className="btn-primary" onClick={onClick} > /* type = "button" significa que si no se le pasa un tipo, por defecto será "button" y no enviará formularios por accidente */
        {texto}
    </button>
    );
}
export default Button;