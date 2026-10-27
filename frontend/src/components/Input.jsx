function Input({label, type = "text", placeholder, value, onChange, required= false, minLength})
{
    return(
    <div className="form-group">

        <label>{label}</label>

        <input type={type} placeholder={placeholder} value={value} onChange={onChange} required={required} minLength={minLength}/>

    </div>

    );
}
export default Input;