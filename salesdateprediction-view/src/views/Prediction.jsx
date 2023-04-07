import React, {  } from "react";
import {Table, Button, Container, Modal, ModalHeader, ModalBody, FormGroup, ModalFooter, Col, Row, Label, Pagination, PaginationItem, PaginationLink} from 'reactstrap'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSearch } from '@fortawesome/free-solid-svg-icons';
import axios from "axios";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";
import Select from 'react-select';
import makeAnimated from 'react-select/animated';
import Moment from "react-moment";
import { ModalTitle } from "react-bootstrap";

//import moment from "moment/moment";

const animatedComponents = makeAnimated();
var optionsShippers = "";
var optionsEmployees = "";
var optionsProducts = "";
const MySwal = withReactContent(Swal)

class Prediction extends React.Component {
  constructor(props) {
    super(props);
      this.state = {
        data: [],
        dataFilter: [],
        dataCustomer: [],
        shipperData: [],
        employeesData: [],
        productsData: [],
        tipoPersonaState: null,
        sort: {
          column: null,
          direction: "asc",
        },
        form:{
          shipName:'',
          shipAddress:'',
          shipCity:'',
          shipCountry:'',
          orderDate:'',
          requiredDate: '',
          shippedDate: '',
          freight:'',
          unitPrice:'',
          qty: '',
          discount: '',
          productId: '',
          empId:'',
          shipperId:'',
          custId:'',
        },
        modalInsertar: false,
        modalViewOrder: false,
        resultadosBusqueda: '',
        busqueda:'',
        selectedOption: null,
        selectedOptionEmployee: null,
        selectedOptionProduct: null,
        currentPage: 1,
        itemsPerPage: 10, 
      };
      this.handleClick = this.handleClick.bind(this);
  }

  handleChangeOption = (selectedOption) => {
    const shipperId = selectedOption.value;
    this.setState(prevState => ({
      form: {
        ...prevState.form,
        shipperId: shipperId
      }
    }));
  };

  handleChangeOptionEmployee = (selectedOptionEmployee) => {
    const empId = selectedOptionEmployee.value;
    this.setState(prevState => ({
      form: {
        ...prevState.form,
        empId: empId
      }
    }));
  };

  handleChangeOptionProduct = (selectedOptionProduct) => {
    const productId = selectedOptionProduct.value;
    this.setState(prevState => ({
      form: {
        ...prevState.form,
        productId: productId
      }
    }));
  };

  getShippers = async () => {
    await axios
      .get("https://localhost:7240/Shippers/GetShippers")
      .then((response) => {
        this.setState({shipperData: response.data})
      })
      .catch((error) => {
        console.log("Error: " + error);
      });
  };
  
  getEmployees = async () => {
    await axios
      .get("https://localhost:7240/Employees/GetEmployees")
      .then((response) => {
        this.setState({employeesData: response.data})
      })
      .catch((error) => {
        console.log("Error: " + error);
      });
  };

  getProducts = async () => {
    await axios
      .get("https://localhost:7240/Products/GetProducts")
      .then((response) => {
        this.setState({productsData: response.data})
      })
      .catch((error) => {
        console.log("Error: " + error);
      });
  };

  handleChange = async e =>{
    e.persist();
      await this.setState({
        form:{
          ...this.state.form,
          [e.target.name]: e.target.value,
        }
      });
  }

  handleSort = (column) => {
    const { sort } = this.state;

    if (sort.column === column) {
      sort.direction = sort.direction === "asc" ? "desc" : "asc";
    } else {
      sort.column = column;
      sort.direction = "asc";
    }

    this.setState({ sort });
  };  

  handleClick(event) {
    this.setState({
      currentPage: Number(event.target.id)
    });
  }

  getCustomerOrder = async () =>{
    await axios
    .get('https://localhost:7240/Customers/GetCustomerOrders')
    .then(response => {
    this.setState({data: response.data});
    this.setState({dataFilter: response.data});  
    });
  }

  handleBuscarChange = async e =>{
    this.setState({busqueda: e.target.value});
    this.filtrar(e.target.value);
  }

  filtrar=(terminoBusqueda)=>{
    var resultadosBusqueda=this.state.data.filter((elemento)=>{
      if(elemento.customerName.toString().toLowerCase().includes(terminoBusqueda.toLowerCase())
      ){
        return elemento;
      }
      return resultadosBusqueda;
    });
    this.setState({dataFilter: resultadosBusqueda});
  }

  modalInsertar = (cusId) => {
    this.setState({modalInsertar: !this.state.modalInsertar});
    this.setState({
      form: {
        custId: cusId
      }
    });
  }

  modalViewOrder = () => {
    this.setState({modalViewOrder: !this.state.modalViewOrder});
  }

  selectOrder=(customer)=>{
    this.getOrdersByIdClient(customer.custId);
  }
  
  getOrdersByIdClient = async (id) => { 
    await axios
    .get("https://localhost:7240/Orders/GetOrdersByIdClient/"+ id)
    .then(response => {
    this.setState({dataCustomer: response.data});
    localStorage.setItem("CustomerName",response.data[0].companyName)
    });
  }

  insert = async () =>{
    //Insert new order
    await axios.post("https://localhost:7240/Orders/AddNewOrder",this.state.form)
    .then((response) => {
      this.modalInsertar();
         MySwal.fire({
            title: <strong>Bien!</strong>,
            html: <i>Registro grabado Correctamente</i>,
            icon: 'success'
          })
        }).catch(error => {
          console.log(error)
      MySwal.fire({
        title: <strong>Error!</strong>,
        html: JSON.stringify(error.message),
        icon: 'error'
      })
    });
  }

  handleSaveButtonClick = () => {
    // Validate inputs
    if (!this.state.form.empId || !this.state.form.shipperId
      || !this.state.form.shipName || !this.state.form.shipAddress
      || !this.state.form.shipCity || !this.state.form.shipCountry
      || !this.state.form.orderDate || !this.state.form.requiredDate
      || !this.state.form.shippedDate || !this.state.form.freight
      || !this.state.form.productId || !this.state.form.unitPrice
      || !this.state.form.qty || !this.state.form.discount) {
        MySwal.fire({
          title: <strong>Advertencia!</strong>,
          html: <i>Debe diligenciar todos los campos!</i>,
          icon: 'warning'
        })
      return;
    }

    this.insert();
  }
  
  componentDidMount(){
    this.getCustomerOrder();
    this.getShippers();
    this.getEmployees();
    this.getProducts();
  }
 
  render() {
    const {form} = this.state;
    const {selectedOption, selectedOptionEmployee, selectedOptionProduct} = this.state;
    const {shipperData, employeesData, productsData} = this.state;
    const { currentPage, itemsPerPage, dataFilter } = this.state;

    // Logic for displaying current items
    const indexOfLastItem = currentPage * itemsPerPage;
    const indexOfFirstItem = indexOfLastItem - itemsPerPage;
    const currentItems = dataFilter.slice(indexOfFirstItem, indexOfLastItem);

    // Logic for displaying page numbers
    const pageNumbers = [];
    for (let i = 1; i <= Math.ceil(dataFilter.length / itemsPerPage); i++) {
      pageNumbers.push(i);
    }

    const { sort } = this.state;

    // Sort customers array
    const sortedCustomers = dataFilter.sort((a, b) => {
      const isAsc = sort.direction === "asc";
      if (sort.column === "customerName") {
        return isAsc
          ? a.customerName.localeCompare(b.customerName)
          : b.customerName.localeCompare(a.customerName);
      } else if (sort.column === "lastOrderDate") {
        return isAsc
          ? new Date(a.lastOrderDate) - new Date(b.lastOrderDate)
          : new Date(b.lastOrderDate) - new Date(a.lastOrderDate);
      } else if (sort.column === "nextPredictedOrder") {
        return isAsc
          ? new Date(a.nextPredictedOrder) - new Date(b.nextPredictedOrder)
          : new Date(b.nextPredictedOrder) - new Date(a.nextPredictedOrder);
      }
      return 0;
    });

    optionsShippers = shipperData.map((elemento) => {
      return { value: `${elemento.shipperID}`, label: `${elemento.companyName}` };
    })

    optionsEmployees = employeesData.map((elemento) => {
      return { value: `${elemento.empid}`, label: `${elemento.fullName}` };
    })

    optionsProducts = productsData.map((elemento) => {
      return { value: `${elemento.productId}`, label: `${elemento.productName}` };
    })
    
  return (
    <div>
      <>
        <Container>
          <Container>
            <h4>Customers</h4>
            <div className="containerInput">
            </div>
            <div className="containerInput">
              <input
                className="form-control inputBuscar"
                value={this.state.busqueda}
                placeholder="Customer Name"
                onChange={this.handleBuscarChange}
              />
              <button className="btn btn-success">
                <FontAwesomeIcon icon={faSearch} />
              </button>
            </div>
          </Container>
          <br></br>

          <div>
            <Table>
              <thead>
                <tr>
                  <th onClick={() => this.handleSort("customerName")}>Customer Name</th>
                  <th onClick={() => this.handleSort("lastOrderDate")}>Last Order Date</th>
                  <th onClick={() => this.handleSort("nextPredictedOrder")}>Next Predicted Order</th>
                  <th></th>
                  <th></th>
                </tr>
              </thead>
              <tbody>
                {currentItems.map((elemento, i) => (
                  <tr key={i}>
                    <td>{elemento.customerName}</td>
                    <td><Moment format="DD/MM/YYYY">{elemento.lastOrderDate}</Moment> </td>
                    <td><Moment format="DD/MM/YYYY">{elemento.nextPredictedOrder}</Moment> </td>
                    <td>
                      <button
                        className="btn btn-danger"
                        onClick={() => {
                          this.selectOrder(elemento);
                          this.modalViewOrder();
                        }}
                      >View Order
                      </button>
                    </td>
                    <td>
                      <button
                        className="btn btn-success"
                        onClick={() => {
                          this.setState({ form: null, tipoModal: "insertar" });
                          const cusId = elemento.custId;
                          
                          this.modalInsertar(cusId);
                        }}
                      >New Order
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </Table>
            <div className="containerInput">
              <Pagination>
                <PaginationItem disabled={currentPage <= 1}>
                  <PaginationLink
                    onClick={e => this.handleClick(e)}
                    previous
                    href="#"
                    id={currentPage - 1}
                  />
                </PaginationItem>
                {pageNumbers.map(number => (
                  <PaginationItem key={number} active={number === currentPage}>
                    <PaginationLink
                      onClick={e => this.handleClick(e)}
                      href="#"
                      id={number}
                    >
                      {number}
                    </PaginationLink>
                  </PaginationItem>
                ))}
                <PaginationItem disabled={currentPage >= pageNumbers.length}>
                  <PaginationLink
                    onClick={e => this.handleClick(e)}
                    next
                    href="#"
                    id={currentPage + 1}
                  />
                </PaginationItem>
              </Pagination>
            </div>
          </div>
        </Container>   

        <Modal isOpen={this.state.modalInsertar}>
          <ModalHeader>
            <ModalTitle>
              <Label>{localStorage.getItem("CustomerName")} - Orders</Label>
            </ModalTitle>
          </ModalHeader>

          <ModalBody>
            <ModalTitle>
              <Label>Order</Label>
            </ModalTitle>
            <Row>
              <Col sm="6" lg="6">
                <FormGroup>
                  <label>Employee *:</label>
                  <Select
                    id="empId"
                    name="empId"
                    defaultValue={form ? selectedOptionEmployee : ""}
                    components={animatedComponents}
                    onChange={(this.handleChangeOptionEmployee)}
                    options={optionsEmployees}
                  />
                </FormGroup>
              </Col>
              <Col sm="6" lg="6">
                <FormGroup>
                  <label>Shipper *</label>
                  <Select
                    id="shipperId"
                    name="shipperId"
                    defaultValue={form ? selectedOption : ""}
                    components={animatedComponents}
                    onChange={this.handleChangeOption}
                    options={optionsShippers}
                    
                  />
                </FormGroup>
              </Col>
            </Row>
            <Row>
              <Col sm="12" lg="12">
                <FormGroup>
                  <label>Ship Name *:</label>
                  <input
                    className="form-control"
                    id="shipName"
                    name="shipName"
                    type="text"
                    onChange={this.handleChange}
                    value={form ? form.shipName : ""}
                  />
                </FormGroup>
              </Col>
            </Row>
            <Row>
              <Col sm="4" lg="4">
                <FormGroup>
                  <label>Ship Address *:</label>
                  <input
                    className="form-control"
                    name="shipAddress"
                    id="shipAddress"
                    type="text"
                    onChange={this.handleChange}
                    value={form ? form.shipAddress : ""}
                  />
                </FormGroup>
              </Col>
              <Col sm="4" lg="4">
                <FormGroup>
                  <label>Ship City *:</label>
                  <input
                    className="form-control"
                    id="shipCity"
                    name="shipCity"
                    type="text"
                    onChange={this.handleChange}
                    value={form ? form.shipCity : ""}
                  />
                </FormGroup>
              </Col>
              <Col sm="4" lg="4">
                <FormGroup>
                  <label>Ship Country *:</label>
                  <input
                    className="form-control"
                    id="shipCountry"
                    name="shipCountry"
                    type="text"
                    onChange={this.handleChange}
                    value={form ? form.shipCountry : ""}
                  />
                </FormGroup>
              </Col>
            </Row>
            <Row>
              <Col sm="4" lg="4">
                <FormGroup>
                  <label>Order Date *:</label>
                  <input
                    className="form-control"
                    id="orderDate"
                    name="orderDate"
                    type="date"
                    onChange={this.handleChange}
                    value={form ? form.orderDate : ""}
                  />
                </FormGroup>
              </Col>
              <Col sm="4" lg="4">
                <FormGroup>
                  <label>Required Date *:</label>
                  <input
                    className="form-control"
                    id="requiredDate"
                    name="requiredDate"
                    type="date"
                    onChange={this.handleChange}
                    value={form ? form.requiredDate : ""}
                  />
                </FormGroup>
              </Col>
              <Col sm="4" lg="4">
                <FormGroup>
                  <label>Shipper Date *:</label>
                  <input
                    className="form-control"
                    id="shippedDate"
                    name="shippedDate"
                    type="date"
                    onChange={this.handleChange}
                    value={form ? form.shippedDate : ""}
                  />
                </FormGroup>
              </Col>
            </Row>
            <Row>
              <Col sm="12" lg="12">
                <FormGroup>
                  <label>$ Freight *:</label>
                  <input
                    className="form-control"
                    id="freight"
                    name="freight"
                    type="number"
                    onChange={this.handleChange}
                    value={form ? form.freight : ""}
                  />
                </FormGroup>
              </Col>
            </Row>
            <ModalTitle>
              <Label>Order Details</Label>
            </ModalTitle>
            <Row>
              <Col sm="12" lg="12">
                <FormGroup>
                  <label>Product *</label>
                  <Select
                    id="productId"
                    name="productId"
                    defaultValue={form ? selectedOptionProduct : ""}
                    components={animatedComponents}
                    onChange={this.handleChangeOptionProduct}
                    options={optionsProducts}
                    
                  />
                </FormGroup>
              </Col>
            </Row>
            <Row>
              <Col sm="4" lg="4">
                <FormGroup>
                  <label>Unit Price *</label>
                  <input
                    className="form-control"
                    id="unitPrice"
                    name="unitPrice"
                    type="number"
                    onChange={this.handleChange}
                    value={form ? form.unitPrice : ""}
                  />
                </FormGroup>
              </Col>
              <Col sm="4" lg="4">
                <FormGroup>
                  <label>Quantity *</label>
                  <input
                    className="form-control"
                    id="qty"
                    name="qty"
                    type="number"
                    onChange={this.handleChange}
                    value={form ? form.qty : ""}
                  />
                </FormGroup>
              </Col>
              <Col sm="4" lg="4">
                <FormGroup>
                  <label>Discount *</label>
                  <input
                    className="form-control"
                    id="discount"
                    name="discount"
                    type="number"
                    onChange={this.handleChange}
                    value={form ? form.discount : ""}
                  />
                </FormGroup>
              </Col>
            </Row>
          </ModalBody>

          <ModalFooter>
          <Button
              className="btn btn-danger"
              onClick={() => this.modalInsertar()}
            >
              Close
            </Button>
            {this.state.tipoModal === "insertar" ? (
              <Button color="success" onClick={() => this.handleSaveButtonClick()}>
                Save
              </Button>
            ) : (
              <Button color="primary" >
                Update
              </Button>
            )}
          </ModalFooter>
        </Modal>

        <Modal size="lg" style={{maxWidth: '80%', width: '80%'}} isOpen={this.state.modalViewOrder}>
          <ModalHeader>
                <Label>{localStorage.getItem("CustomerName")} - Orders</Label>
          </ModalHeader>

          <ModalBody>
            <Row>
              <Col sm="12" lg="12">
                <Container>
                  <Table>
                    <thead>
                      <tr>
                        <th>Order ID</th>
                        <th>Required Date</th>
                        <th>Shipped Date</th>
                        <th>Ship Name</th>
                        <th>Ship Address</th>
                        <th>Ship City</th>
                        <th>Name</th>
                      </tr>
                    </thead>
                    <tbody>
                      {this.state.dataCustomer.map((elemento, i) => (
                        <tr key={i}>
                          <td>{elemento.orderId}</td>
                          <td><Moment format="DD/MM/YYYY HH:mm:ss">{elemento.requiredDate}</Moment> </td>
                          <td><Moment format="DD/MM/YYYY HH:mm:ss">{elemento.shippedDate}</Moment> </td>
                          <td>{elemento.shipName}</td>
                          <td>{elemento.shipAddress}</td>
                          <td>{elemento.shipCity}</td>
                          <td>{elemento.companyName}</td>
                        </tr>
                      ))}
                    </tbody>
                  </Table>
                </Container>
              </Col>
            </Row>
          </ModalBody>

          <ModalFooter>
            <Button
              className="btn btn-danger"
              onClick={() => this.modalViewOrder()}
            >
              Close
            </Button>
          </ModalFooter>
        </Modal>
      </>
    </div>
  );
};
}
export default Prediction;