import logo from "../assets/bugSlayer.png"

function Footer()
{
    return(
    <footer>
      <img src={logo} alt="Logo del equipo BugSlayer" className="logo-footer" />
      <p>by BugSlayer team</p>
    </footer>
    );
}
export default Footer;