function Select({label="Seleccionar", value, onChange, options, required = false }) 
{
    return(
        <div className="form-group">
      <label>{label}</label>
      <select value={value} onChange={onChange} required={required} className="select-rol" >
        {/*recorre la lista de opciones y crea un <option> por cada una*/}
        {options.map((opcion) => (
          <option key={opcion.value} value={opcion.value}>
            {opcion.label}
          </option>
        ))}
      </select>
    </div>
    );
}
export default Select;