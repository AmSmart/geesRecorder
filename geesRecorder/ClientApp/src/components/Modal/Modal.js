import React, { useState } from "react";
import "./Modal.css";

import Modal from "react-modal";

export default class Modal extends React.Component {
    state = { show: false };
  
    showModal = () => {
      this.setState({ show: true });
    };
  
    hideModal = () => {
      this.setState({ show: false });
    };
  
    render() {
      return (
        <main>
          <h1>React Modal</h1>
          <Modal show={this.state.show} handleClose={this.hideModal}>
            <p>Modal</p>
            <p>Data</p>
          </Modal>
          <button type="button" onClick={this.showModal}>
            Open
          </button>
        </main>
      );
    }
  }
  
  const Modal = ({ handleClose, show, children }) => {
    const showHideClassName = show ? "modal display-block" : "modal display-none";
    return (
        <div className={showHideClassName}>
          <section className="modal-main">
            {children}
            <button onClick={handleClose}>Close</button>
          </section>
        </div>
      );
    };
    
    const container = document.createElement("div");
    document.body.appendChild(container);
    ReactDOM.render(<App />, container);
    
    
    