using System;
using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TakeOffAPI.DatabaseUtility;

namespace TakeOffAPI.Entities
{
    [Table("factory_fit_test")]
	public class FactoryFit
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Name("operatorID")]
        public string operatorID { get; set; } = string.Empty;
        [Name("jobday")]
        public string jobday { get; set; } = string.Empty;
        [Name("jobtime")]
        public string jobtime { get; set; } = string.Empty;
        public string duration { get; set; } = string.Empty;
        public string receiverName { get; set; } = string.Empty;
        public string receiverEmail { get; set; } = string.Empty;
        public string stationName { get; set; } = StationEnum.TakeOff;
        [Name("jobno")]
        public string jobno { get; set; } = string.Empty;
        [Name("drawingno")]
        public string drawingno { get; set; } = string.Empty;
        [Name("handle")]
        public string handle { get; set; } = string.Empty;
        [Name("itemno")]
        public string itemno { get; set; } = string.Empty;
        [Name("insulation")]
        public string insulation { get; set; } = string.Empty;
        [Name("galvenized")]
        public string galvenized { get; set; } = string.Empty;
        [Name("notes")]
        public string notes { get; set; } = string.Empty;
        [Name("weight")]
        public string weight { get; set; } = string.Empty;
        [Name("status")]
        public string status { get; set; } = string.Empty;
        [Name("itemQuantity")]
        public int qty { get; set; } = 0;
        [Name("cuttype")]
        public string cuttype { get; set; } = string.Empty;
        [Name("cid")]
        public string cid { get; set; } = string.Empty;
        [Name("description")]
        public string description { get; set; } = string.Empty;
        [Name("doublewall")]
        public string doublewall { get; set; } = string.Empty;
        [Name("pathId")]
        public int pathId { get; set; } = 0;
        [Name("insulationarea")]
        public string insulationarea { get; set; } = string.Empty;
        [Name("metalarea")]
        public string metalarea { get; set; } = string.Empty;
        [Name("boughtout")]
        public string boughtout { get; set; } = string.Empty;
        [Name("linearmeter")]
        public string linearmeter { get; set; } = string.Empty;
        [Name("sectionindex")]
        public string sectionindex { get; set; } = string.Empty;
        [Name("sectiondescription")]
        public string sectiondescription { get; set; } = string.Empty;
        [Name("prefixstring")]
        public string prefixstring { get; set; } = string.Empty;
        [Name("insulationSpec")]
        public string insulationSpec { get; set; } = string.Empty;
        [Name("widthDim")]
        public string widthDim { get; set; } = string.Empty;
        [Name("depthDim")]
        public string depthDim { get; set; } = string.Empty;
        [Name("lengthangle")]
        public string lengthangle { get; set; } = string.Empty;
        [Name("connector")]
        public string connector { get; set; } = string.Empty;
        [Name("material")]
        public string material { get; set; } = string.Empty;
        [Name("equipmentTag")]
        public string equipmentTag { get; set; } = string.Empty;
        [Name("jobArea")]
        public string jobArea { get; set; } = string.Empty;
        public string storageInfo { get; set; } = string.Empty;
        public string packingID { get; set; } = string.Empty;
        public string resetDay { get; set; } = string.Empty;
        public string resetTime { get; set; } = string.Empty;
        [Name("filename")]
        public string filename { get; set; } = string.Empty;
        public string prodQABy { get; set; } = string.Empty;
        public string prodQADay { get; set; } = string.Empty;
        public string prodQATime { get; set; } = string.Empty;
        public FactoryFit()
		{
		}

        public FactoryFit(int id, string operatorID, string jobday, string jobtime, string duration, string receiverName, string receiverEmail, string stationName, string jobno, string drawingno, string handle, string itemno, string insulation, string galvenized, string notes, string weight, string status, int qty, string cuttype, string cid, string description, string doublewall, int pathId, string insulationarea, string metalarea, string boughtout, string linearmeter, string sectionindex, string sectiondescription, string prefixstring, string insulationSpec, string widthDim, string depthDim, string lengthangle, string connector, string material, string equipmentTag, string jobArea, string storageInfo, string packingID, string resetDay, string resetTime, string filename, string prodQABy, string prodQADay, string prodQATime)
        {
            this.id = id;
            this.operatorID = operatorID;
            this.jobday = jobday;
            this.jobtime = jobtime;
            this.duration = duration;
            this.receiverName = receiverName;
            this.receiverEmail = receiverEmail;
            this.stationName = stationName;
            this.jobno = jobno;
            this.drawingno = drawingno;
            this.handle = handle;
            this.itemno = itemno;
            this.insulation = insulation;
            this.galvenized = galvenized;
            this.notes = notes;
            this.weight = weight;
            this.status = status;
            this.qty = qty;
            this.cuttype = cuttype;
            this.cid = cid;
            this.description = description;
            this.doublewall = doublewall;
            this.pathId = pathId;
            this.insulationarea = insulationarea;
            this.metalarea = metalarea;
            this.boughtout = boughtout;
            this.linearmeter = linearmeter;
            this.sectionindex = sectionindex;
            this.sectiondescription = sectiondescription;
            this.prefixstring = prefixstring;
            this.insulationSpec = insulationSpec;
            this.widthDim = widthDim;
            this.depthDim = depthDim;
            this.lengthangle = lengthangle;
            this.connector = connector;
            this.material = material;
            this.equipmentTag = equipmentTag;
            this.jobArea = jobArea;
            this.storageInfo = storageInfo;
            this.packingID = packingID;
            this.resetDay = resetDay;
            this.resetTime = resetTime;
            this.filename = filename;
            this.prodQABy = prodQABy;
            this.prodQADay = prodQADay;
            this.prodQATime = prodQATime;
        }

        public FactoryFit(DispatchDetail newObject)
        {
            this.operatorID = newObject.operatorID;
            this.jobday = newObject.jobday;
            this.jobtime = newObject.jobtime;
            this.duration = newObject.duration;
            this.receiverName = newObject.receiverName;
            this.receiverEmail = newObject.receiverEmail;
            this.stationName = newObject.stationName;
            this.jobno = newObject.jobno;
            this.drawingno = newObject.drawingno;
            this.handle = newObject.handle;
            this.itemno = newObject.itemno;
            this.insulation = newObject.insulation;
            this.galvenized = newObject.galvenized;
            this.notes = newObject.notes;
            this.weight = newObject.weight;
            this.status = newObject.status;
            this.qty = newObject.qty;
            this.cuttype = newObject.cuttype;
            this.cid = newObject.cid;
            this.description = newObject.description;
            this.doublewall = newObject.doublewall;
            this.pathId = newObject.pathId;
            this.insulationarea = newObject.insulationarea;
            this.metalarea = newObject.metalarea;
            this.boughtout = newObject.boughtout;
            this.linearmeter = newObject.linearmeter;
            this.sectionindex = newObject.sectionindex;
            this.sectiondescription = newObject.sectiondescription;
            this.prefixstring = newObject.prefixstring;
            this.insulationSpec = newObject.insulationSpec;
            this.widthDim = newObject.widthDim;
            this.depthDim = newObject.depthDim;
            this.lengthangle = newObject.lengthangle;
            this.connector = newObject.connector;
            this.material = newObject.material;
            this.equipmentTag = newObject.equipmentTag;
            this.jobArea = newObject.jobArea;
            this.storageInfo = newObject.storageInfo;
            this.packingID = newObject.packingID;
            this.resetDay = newObject.resetDay;
            this.resetTime = newObject.resetTime;
            this.filename = newObject.filename;
            this.prodQABy = newObject.prodQABy;
            this.prodQADay = newObject.prodQADay;
            this.prodQATime = newObject.prodQATime;
        }
    }
}

