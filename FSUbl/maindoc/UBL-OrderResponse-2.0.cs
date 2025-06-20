// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
//
//     http://FS.codeplex.com/
//
// </auto-generated>
//------------------------------------------------------------------------------
namespace FSUbl
{
    using FSUbl.Cac;
    
    
    /// <summary>
    ///  The document used to indicate detailed acceptance or rejection of an Order or to make a counter-offer.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("ublxsd", "2.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:OrderResponse-2")]
    [System.Xml.Serialization.XmlRootAttribute("OrderResponse", Namespace="urn:oasis:names:specification:ubl:schema:xsd:OrderResponse-2", IsNullable=false)]
    public partial class OrderResponseType : UblBaseDocumentType
    {
        
        private Udt.IdentifierType idField;
        
        private Udt.IdentifierType salesOrderIDField;
        
        private Udt.IndicatorType copyIndicatorField;
        
        private Udt.IdentifierType uUIDField;
        
        private Udt.DateType issueDateField;
        
        private Udt.TimeType issueTimeField;
        
        private Udt.TextType[] noteField;
        
        private Qdt.CurrencyCodeType documentCurrencyCodeField;
        
        private Qdt.CurrencyCodeType pricingCurrencyCodeField;
        
        private Qdt.CurrencyCodeType taxCurrencyCodeField;
        
        private Udt.QuantityType totalPackagesQuantityField;
        
        private Udt.MeasureType grossWeightMeasureField;
        
        private Udt.MeasureType netWeightMeasureField;
        
        private Udt.MeasureType netNetWeightMeasureField;
        
        private Udt.MeasureType grossVolumeMeasureField;
        
        private Udt.MeasureType netVolumeMeasureField;
        
        private Udt.TextType customerReferenceField;
        
        private Udt.CodeType accountingCostCodeField;
        
        private Udt.TextType accountingCostField;
        
        private Udt.NumericType lineCountNumericField;
        
        private PeriodType[] validityPeriodField;
        
        private OrderReferenceType[] orderReferenceField;
        
        private DocumentReferenceType[] orderDocumentReferenceField;
        
        private DocumentReferenceType originatorDocumentReferenceField;
        
        private DocumentReferenceType[] additionalDocumentReferenceField;
        
        private ContractType[] contractField;
        
        private SignatureType[] signatureField;
        
        private SupplierPartyType sellerSupplierPartyField;
        
        private CustomerPartyType buyerCustomerPartyField;
        
        private CustomerPartyType originatorCustomerPartyField;
        
        private PartyType freightForwarderPartyField;
        
        private SupplierPartyType accountingSupplierPartyField;
        
        private CustomerPartyType accountingCustomerPartyField;
        
        private DeliveryType[] deliveryField;
        
        private DeliveryTermsType deliveryTermsField;
        
        private PaymentMeansType paymentMeansField;
        
        private AllowanceChargeType[] allowanceChargeField;
        
        private TransactionConditionsType transactionConditionsField;
        
        private CountryType destinationCountryField;
        
        private TaxTotalType[] taxTotalField;
        
        private MonetaryTotalType legalMonetaryTotalField;
        
        private OrderLineType[] orderLineField;
        
        /// <summary>
        ///  An identifier for the Order Response assigned by the Seller.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Udt.IdentifierType ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
        
        /// <summary>
        ///  An identifier for the Order issued by the Seller.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Udt.IdentifierType SalesOrderID
        {
            get
            {
                return this.salesOrderIDField;
            }
            set
            {
                this.salesOrderIDField = value;
            }
        }
        
        /// <summary>
        ///  Indicates whether the Order Response is a copy (true) or not (false).
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Udt.IndicatorType CopyIndicator
        {
            get
            {
                return this.copyIndicatorField;
            }
            set
            {
                this.copyIndicatorField = value;
            }
        }
        
        /// <summary>
        ///  A universally unique identifier for an instance of this ABIE.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Udt.IdentifierType UUID
        {
            get
            {
                return this.uUIDField;
            }
            set
            {
                this.uUIDField = value;
            }
        }
        
        /// <summary>
        ///  The date assigned by the Seller on which the Order was responded to.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Udt.DateType IssueDate
        {
            get
            {
                return this.issueDateField;
            }
            set
            {
                this.issueDateField = value;
            }
        }
        
        /// <summary>
        ///  The time assigned by the Seller at which the Order was responded to.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Udt.TimeType IssueTime
        {
            get
            {
                return this.issueTimeField;
            }
            set
            {
                this.issueTimeField = value;
            }
        }
        
        /// <summary>
        ///  Free-form text applying to the Order Response. This element may contain notes or any other similar information that is not contained explicitly in another structure.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Note", Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Udt.TextType[] Note
        {
            get
            {
                return this.noteField;
            }
            set
            {
                this.noteField = value;
            }
        }
        
        /// <summary>
        ///  The default currency for the Order Response.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Qdt.CurrencyCodeType DocumentCurrencyCode
        {
            get
            {
                return this.documentCurrencyCodeField;
            }
            set
            {
                this.documentCurrencyCodeField = value;
            }
        }
        
        /// <summary>
        ///  The currency that is used for all prices in the Order Response.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Qdt.CurrencyCodeType PricingCurrencyCode
        {
            get
            {
                return this.pricingCurrencyCodeField;
            }
            set
            {
                this.pricingCurrencyCodeField = value;
            }
        }
        
        /// <summary>
        ///  The currency that is used for all tax amounts in the Order Response.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Qdt.CurrencyCodeType TaxCurrencyCode
        {
            get
            {
                return this.taxCurrencyCodeField;
            }
            set
            {
                this.taxCurrencyCodeField = value;
            }
        }
        
        /// <summary>
        ///  The total number of packages contained in the Order Response.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Udt.QuantityType TotalPackagesQuantity
        {
            get
            {
                return this.totalPackagesQuantityField;
            }
            set
            {
                this.totalPackagesQuantityField = value;
            }
        }
        
        /// <summary>
        ///  The total gross weight for the Order Response (goods + packaging + transport equipment).
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Udt.MeasureType GrossWeightMeasure
        {
            get
            {
                return this.grossWeightMeasureField;
            }
            set
            {
                this.grossWeightMeasureField = value;
            }
        }
        
        /// <summary>
        ///  The total net weight for the Order Response (goods + packaging).
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Udt.MeasureType NetWeightMeasure
        {
            get
            {
                return this.netWeightMeasureField;
            }
            set
            {
                this.netWeightMeasureField = value;
            }
        }
        
        /// <summary>
        ///  The total net weight of the goods in the Order Response excluding packaging.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Udt.MeasureType NetNetWeightMeasure
        {
            get
            {
                return this.netNetWeightMeasureField;
            }
            set
            {
                this.netNetWeightMeasureField = value;
            }
        }
        
        /// <summary>
        ///  The total volume of the goods in the Order Response including packaging.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Udt.MeasureType GrossVolumeMeasure
        {
            get
            {
                return this.grossVolumeMeasureField;
            }
            set
            {
                this.grossVolumeMeasureField = value;
            }
        }
        
        /// <summary>
        ///  The total volume of the goods in the Order Response excluding packaging.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Udt.MeasureType NetVolumeMeasure
        {
            get
            {
                return this.netVolumeMeasureField;
            }
            set
            {
                this.netVolumeMeasureField = value;
            }
        }
        
        /// <summary>
        ///  A supplementary reference assigned by the Buyer, e.g. the CRI in a purchasing card transaction.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Udt.TextType CustomerReference
        {
            get
            {
                return this.customerReferenceField;
            }
            set
            {
                this.customerReferenceField = value;
            }
        }
        
        /// <summary>
        ///  An accounting cost code applied to the order as a whole.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Udt.CodeType AccountingCostCode
        {
            get
            {
                return this.accountingCostCodeField;
            }
            set
            {
                this.accountingCostCodeField = value;
            }
        }
        
        /// <summary>
        ///  An accounting cost code applied to the order as a whole, expressed as text.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Udt.TextType AccountingCost
        {
            get
            {
                return this.accountingCostField;
            }
            set
            {
                this.accountingCostField = value;
            }
        }
        
        /// <summary>
        ///  The number of lines in the document.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Udt.NumericType LineCountNumeric
        {
            get
            {
                return this.lineCountNumericField;
            }
            set
            {
                this.lineCountNumericField = value;
            }
        }
        
        /// <summary>
        ///  The period for which the Order Response is valid.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("ValidityPeriod", Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public PeriodType[] ValidityPeriod
        {
            get
            {
                return this.validityPeriodField;
            }
            set
            {
                this.validityPeriodField = value;
            }
        }
        
        /// <summary>
        ///  An association to Order Reference (the reference of the Order being responded to).
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("OrderReference", Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public OrderReferenceType[] OrderReference
        {
            get
            {
                return this.orderReferenceField;
            }
            set
            {
                this.orderReferenceField = value;
            }
        }
        
        /// <summary>
        ///  An associative reference to [another] Order.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("OrderDocumentReference", Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public DocumentReferenceType[] OrderDocumentReference
        {
            get
            {
                return this.orderDocumentReferenceField;
            }
            set
            {
                this.orderDocumentReferenceField = value;
            }
        }
        
        /// <summary>
        ///  An associative reference to Originator Document.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public DocumentReferenceType OriginatorDocumentReference
        {
            get
            {
                return this.originatorDocumentReferenceField;
            }
            set
            {
                this.originatorDocumentReferenceField = value;
            }
        }
        
        /// <summary>
        ///  An associative reference to Additional Document.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("AdditionalDocumentReference", Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public DocumentReferenceType[] AdditionalDocumentReference
        {
            get
            {
                return this.additionalDocumentReferenceField;
            }
            set
            {
                this.additionalDocumentReferenceField = value;
            }
        }
        
        /// <summary>
        ///  An association to Contract
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Contract", Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public ContractType[] Contract
        {
            get
            {
                return this.contractField;
            }
            set
            {
                this.contractField = value;
            }
        }
        
        /// <summary>
        ///  An association to Signature.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Signature", Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public SignatureType[] Signature
        {
            get
            {
                return this.signatureField;
            }
            set
            {
                this.signatureField = value;
            }
        }
        
        /// <summary>
        ///  An association to the Seller.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public SupplierPartyType SellerSupplierParty
        {
            get
            {
                return this.sellerSupplierPartyField;
            }
            set
            {
                this.sellerSupplierPartyField = value;
            }
        }
        
        /// <summary>
        ///  An association to the Buyer.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public CustomerPartyType BuyerCustomerParty
        {
            get
            {
                return this.buyerCustomerPartyField;
            }
            set
            {
                this.buyerCustomerPartyField = value;
            }
        }
        
        /// <summary>
        ///  An association to the Originator.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public CustomerPartyType OriginatorCustomerParty
        {
            get
            {
                return this.originatorCustomerPartyField;
            }
            set
            {
                this.originatorCustomerPartyField = value;
            }
        }
        
        /// <summary>
        ///  An association to a Freight Forwarder or Carrier.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public PartyType FreightForwarderParty
        {
            get
            {
                return this.freightForwarderPartyField;
            }
            set
            {
                this.freightForwarderPartyField = value;
            }
        }
        
        /// <summary>
        ///  An association to the Accounting Supplier Party.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public SupplierPartyType AccountingSupplierParty
        {
            get
            {
                return this.accountingSupplierPartyField;
            }
            set
            {
                this.accountingSupplierPartyField = value;
            }
        }
        
        /// <summary>
        ///  An association to the Accounting Customer Party.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public CustomerPartyType AccountingCustomerParty
        {
            get
            {
                return this.accountingCustomerPartyField;
            }
            set
            {
                this.accountingCustomerPartyField = value;
            }
        }
        
        /// <summary>
        ///  An association to Delivery.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Delivery", Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public DeliveryType[] Delivery
        {
            get
            {
                return this.deliveryField;
            }
            set
            {
                this.deliveryField = value;
            }
        }
        
        /// <summary>
        ///  An association to Delivery Terms.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public DeliveryTermsType DeliveryTerms
        {
            get
            {
                return this.deliveryTermsField;
            }
            set
            {
                this.deliveryTermsField = value;
            }
        }
        
        /// <summary>
        ///  An association to Payment Means.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public PaymentMeansType PaymentMeans
        {
            get
            {
                return this.paymentMeansField;
            }
            set
            {
                this.paymentMeansField = value;
            }
        }
        
        /// <summary>
        ///  An association to Allowances and Charges that apply to the Order Response as a whole.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("AllowanceCharge", Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public AllowanceChargeType[] AllowanceCharge
        {
            get
            {
                return this.allowanceChargeField;
            }
            set
            {
                this.allowanceChargeField = value;
            }
        }
        
        /// <summary>
        ///  An association with any sales or purchasing conditions applying to the whole order.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public TransactionConditionsType TransactionConditions
        {
            get
            {
                return this.transactionConditionsField;
            }
            set
            {
                this.transactionConditionsField = value;
            }
        }
        
        /// <summary>
        ///  Associates the order response with the country to which it is destined, for Customs purposes.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public CountryType DestinationCountry
        {
            get
            {
                return this.destinationCountryField;
            }
            set
            {
                this.destinationCountryField = value;
            }
        }
        
        /// <summary>
        ///  An association to the total tax amount of the Order (as calculated by the Seller).
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("TaxTotal", Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public TaxTotalType[] TaxTotal
        {
            get
            {
                return this.taxTotalField;
            }
            set
            {
                this.taxTotalField = value;
            }
        }
        
        /// <summary>
        ///  An association to the total amounts for the Order (or counter-offer).
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public MonetaryTotalType LegalMonetaryTotal
        {
            get
            {
                return this.legalMonetaryTotalField;
            }
            set
            {
                this.legalMonetaryTotalField = value;
            }
        }
        
        /// <summary>
        ///  An association to one or more Order Lines.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("OrderLine", Namespace="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public OrderLineType[] OrderLine
        {
            get
            {
                return this.orderLineField;
            }
            set
            {
                this.orderLineField = value;
            }
        }
    }
}
