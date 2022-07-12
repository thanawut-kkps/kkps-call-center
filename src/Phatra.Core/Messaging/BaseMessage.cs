using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Xml.Linq;
using System.Xml;

namespace Phatra.Core.Messaging
{
    /// <summary>
    /// Base class สำหรับแสดง message ที่จะใช้ในหน้า UI หรือข้อความต่างๆที่จะใช้ในระบบ *** จุคประสงค์หลักคือใช้แสดง message ในหน้า UI เช่น เพิ่มข้อมูลเสร็จสิ้น ลบข้อมูลเสร็จสิ้นเป็นต้น
    /// </summary>
    /// <remarks>
    ///     1.ให้สร้าง class ที่ inherit มาจาก BaseMessage
    ///     2.ให้สร้าง static proprty เพื่อทำการ create instance ของ class info แค่อันเดียว
    ///     3.ให้ทำการ override property XmlMessagePath เพื่อบอกว่า path ของ xml message นั้นอยู่ที่ไหน
    ///     ดูตัวอย่างได้จาก Example Message.cs และไฟล์ xml ได้จาก Example Message.xml
    /// </remarks>
    public class BaseMessage
    {

        /// <summary>
        /// Enum ที่ใช้ในการจัดกลุ่มประเภทของ message ตอนที่จะทำการดึง message ออกมาต้องระบุไปว่าประเภทของ message ที่จะเอามาแสดงคืออะไร
        /// </summary>
        public enum MessageTypeEnum
        {
            Information = 0,
            Warning = 1,
            Error = 2
        }

        /// <summary>
        /// Property ที่บอกว่าตอนนี้ folder ของ dll ที่กำลัง execute นั้นอยู่ที่ไหน
        /// </summary>
        protected string ExecutingPath
        {
            get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\", ""); }
        }

        /// <summary>
        /// Property folder xml ที่เก็บ message 
        /// </summary>
        protected virtual string XmlMessageFilePath
        {
            get { return Path.Combine(this.ExecutingPath, "MessageInfo.xml"); }
        }

        /// <summary>
        /// Method ที่ใช้ในการดึง message ที่อยู่ใน file xml
        /// </summary>
        /// <param name="messageGroupName">ชื่อของ message group name</param>
        /// <param name="pageName">ชื่อของ page name หรือจะเป็น name string อะไรก็ได้ที่ตรงกับใน xml </param>
        /// <param name="messageType">ประเภทของ message</param>
        /// <param name="id">id ของแต่ละ message</param>
        /// <returns>message ที่เก็บอยู่ใน xml</returns>
        /// <example>
        ///     ตัวอย่างไฟล์ xml
        ///     
        /// <?xml version="1.0" encoding="utf-8" ?>
        ///<messages>
        ///  <messageGroup name="PFS.Web.CompanyPdf.Forms.Load">
        ///    <page name="ImportCompanyPdf">
        ///      <information>
        ///        <message id="I001">{0} มีจำนวน file ที่จะ import {1} file</message>
        ///        <message id="I002">'{0}' ที่เลือกไม่มี file ที่จะ import</message>
        ///        <message id="I003">นำเข้าข้อมูลเสร็จสิ้นแล้ว</message>
        ///      </information>
        ///      <warning>
        ///        <message id="W001"></message>
        ///      </warning>
        ///      <error>
        ///        <message id="E001"></message>
        ///      </error>
        ///    </page>
        ///  </messageGroup>
        ///
        ///  <messageGroup name="PFS.Web.CompanyPdf.Forms.Views">
        ///    <page name="CompanyPdfGridView">
        ///      <information>
        ///        <message id="I001">ทำการ Approve Status เสร็จสิ้น</message>
        ///      </information>
        ///      <warning>
        ///        <message id="W001"></message>
        ///      </warning>
        ///      <error>
        ///        <message id="E001"></message>
        ///      </error>
        ///    </page>
        ///  </messageGroup>
        ///</messages>
        /// 
        /// </example>
        public string GetMessage(string messageGroupName, string pageName, MessageTypeEnum messageType, string id)
        {
            return this.GetMessage(messageGroupName, pageName, messageType, id, null);
        }

        /// <summary>
        /// Method ที่ใช้ในการดึง message ที่อยู่ใน file xml
        /// </summary>
        /// <param name="messageGroupName">ชื่อของ message group name</param>
        /// <param name="pageName">ชื่อของ page name</param>
        /// <param name="messageType">ประเภทของ message</param>
        /// <param name="id">id ของแต่ละ message</param>
        /// <param name="additionalContents">string ที่จะใช้ในการ concat กับ message</param>
        /// <returns>message ที่เก็บอยู่ใน xml</returns>
        /// <example>
        ///     ตัวอย่างไฟล์ xml
        ///     
        /// <?xml version="1.0" encoding="utf-8" ?>
        ///<messages>
        ///  <messageGroup name="PFS.Web.CompanyPdf.Forms.Load">
        ///    <page name="ImportCompanyPdf">
        ///      <information>
        ///        <message id="I001">{0} มีจำนวน file ที่จะ import {1} file</message>
        ///        <message id="I002">'{0}' ที่เลือกไม่มี file ที่จะ import</message>
        ///        <message id="I003">นำเข้าข้อมูลเสร็จสิ้นแล้ว</message>
        ///      </information>
        ///      <warning>
        ///        <message id="W001"></message>
        ///      </warning>
        ///      <error>
        ///        <message id="E001"></message>
        ///      </error>
        ///    </page>
        ///  </messageGroup>
        ///
        ///  <messageGroup name="PFS.Web.CompanyPdf.Forms.Views">
        ///    <page name="CompanyPdfGridView">
        ///      <information>
        ///        <message id="I001">ทำการ Approve Status เสร็จสิ้น</message>
        ///      </information>
        ///      <warning>
        ///        <message id="W001"></message>
        ///      </warning>
        ///      <error>
        ///        <message id="E001"></message>
        ///      </error>
        ///    </page>
        ///  </messageGroup>
        ///</messages>
        /// 
        /// </example>
        public string GetMessage(string messageGroupName, string pageName, MessageTypeEnum messageType, string id, params string[] additionalContents)
        {
            string xmlPath = this.XmlMessageFilePath;
            if (Thread.CurrentThread.CurrentUICulture.Name != string.Empty)
            {
                xmlPath = Path.Combine(Path.GetDirectoryName(this.XmlMessageFilePath), Path.GetFileNameWithoutExtension(this.XmlMessageFilePath)
                    + "." + Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName
                    + Path.GetExtension(this.XmlMessageFilePath));
            }
            //---ทำการค้นหาไฟล์ xml message ก่อนว่ามีหรือไม่
            if (!File.Exists(xmlPath)) throw new FileNotFoundException("Xml Message file not found " + xmlPath);

            XElement root = XElement.Load(xmlPath);

            //---ทำการค้นหาว่ามี node ที่ชื่อ messageGroup หรือไม่
            var msgGroup = from grp in root.Elements("messageGroup") where (string)grp.Attribute("name") == messageGroupName select grp;
            if (msgGroup.Count() == 0) throw new XmlException("Xml Message file error Node <messageGroup name='" + messageGroupName + "'> not found.");

            //---ทำการค้นหาว่ามี node ที่ขื่อ page หรือไม่
            var msgPage = from pName in msgGroup.Elements("page") where (string)pName.Attribute("name") == pageName select pName;
            if (msgPage.Count() == 0) throw new XmlException("Xml Message file error Node <messageGroup name='" + messageGroupName + "'><page name='" + pageName + "'> not found.");

            //---ทำการค้นหาว่ามี node ของ message type ที่เก็บประเภทของ message ไว้หรือไม่
            string nodeTypeName = this.GetNodeMessageTypeName(messageType);
            var nodeMsgType = from nodeType in msgPage.Elements(nodeTypeName) select nodeType;
            if (nodeMsgType.Count() == 0) throw new XmlException("Xml Message file error Node <messageGroup name='" + messageGroupName + "'><page name='" + pageName + "'><" + nodeTypeName + "> not found.");

            //---ทำการค้นหาว่ามี node message และ id ตามที่ระบุไว้หรือไม่
            var nodeMessage = from msg in nodeMsgType.Elements("message") where (string)msg.Attribute("id") == id select msg;
            if (nodeMessage.Count() == 0) throw new XmlException("Xml Message file error Node <messageGroup name='" + messageGroupName + "'><page name='" + pageName + "'><" + nodeTypeName + "><message id='" + id + "'> not found.");

            string message = string.Empty;
            message = nodeMessage.First().Value;

            //---ถ้ามีการส่ง parameter ที่จะทำการ concat กับ message
            if (additionalContents != null)
            {
                for (int i = 0; i < additionalContents.Length; i++)
                {
                    message = message.Replace("{" + i.ToString() + "}", additionalContents[i]);
                }
            }
            return message;
        }

        /// <summary>
        /// method สำหรับแปลง message type เป็นชื่อ node name
        /// </summary>
        /// <param name="messageType">enum message type</param>
        /// <returns>string ของชื่อ enum</returns>
        private string GetNodeMessageTypeName(MessageTypeEnum messageType)
        {
            //if (messageType == MessageTypeEnum.Information)
            //    return "information";
            //else if (messageType == MessageTypeEnum.Warning)
            //    return "warning";
            //else if (messageType == MessageTypeEnum.Error)
            //    return "error";
            //return string.Empty;
            return Enum.GetName(typeof(MessageTypeEnum), messageType).ToLower();
        }
    }
}
