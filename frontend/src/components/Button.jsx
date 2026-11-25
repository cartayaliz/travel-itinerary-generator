function Button({texto, onClick, type= "button", className = "btn-primary", title, children})
{
    return(
    <button type={type} className={className} onClick={onClick} title={title}> 
    
    {/*type = "button" significa que si no se le pasa un tipo, por defecto será "button" 
    y no enviará formularios por accidente */}

    {/* Si le pasamos un ´children´(como icono) lo dibuja. Si no, dibuja el texto*/}

    {children ? children : texto}
    </button>
    );
}
export default Button;