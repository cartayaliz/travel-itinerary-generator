import jsPDF from "jspdf";
import html2canvas from "html2canvas";
import Button from "./Button";

function ExportButton({ elementId, fileName = "report.pdf" }) {
  const handleExport = () => {
    const input = document.getElementById(elementId);
    if (!input) return;

    html2canvas(input, { scale: 2 }).then((canvas) => {
      const imgData = canvas.toDataURL("image/png");
      const pdf = new jsPDF("p", "mm", "a4"); 
      
      const pdfWidth = pdf.internal.pageSize.getWidth();
      const pdfHeight = (canvas.height * pdfWidth) / canvas.width;

      pdf.addImage(imgData, "PNG", 0, 0, pdfWidth, pdfHeight);
      pdf.save(fileName);
    });
  };

  return (
    <button 
      onClick={handleExport} 
      className="btn-export-icon" 
      title="Export to PDF" 
    >
      ⬇️
    </button>
  );
}

export default ExportButton;