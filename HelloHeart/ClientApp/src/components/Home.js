import React, { useEffect, useState } from 'react';
import StyledBtn from '../Shared/Button'
import './Home.css'

export default function Home() {

  const [testInput, setTestInput] = useState("");
  const [testNumber, setTestNumber] = useState("");

  useEffect(() => {
    populateBloodTestData();
  },[])

  function onSubmit(e) {
    e.preventDefault();
    if(!testInput || !testNumber) return;
    populateBloodTestData(testInput, testNumber)
    resetStates();
    
  }

  function resetStates() {
    setTestInput("")
    setTestNumber("");
  }

  async function populateBloodTestData(testInput, testNumber) {
    const bloodTest = { TestInput: testInput, TestNumber: testNumber }
    const request = {
      method: 'POST',
      Accept: 'application/json',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(bloodTest)
    };
    fetch('bloodtest/setResults', request)
      .then(response => response.json())
      .then(data => console.log(data));


    // console.log("results sent to backend: ", results)
    // const response = await fetch(`bloodtest/${results.testInput}/${results.testNumber}`);
    // const data = await response.json();
    // console.log(response, data);
  }

  return (
    <div className="home-container">
      <form className="input-test-form" onSubmit={onSubmit}>
        <label className="label-header">Testing Type: </label>
        <input className="input-field" type='text'value={testInput} onChange={e => setTestInput(e.target.value)}></input>
        <label className="label-header">Testing Numiric Result: </label>
        <input className="input-field" type='number' value={testNumber} onChange={e => setTestNumber(e.target.value)}></input>
        <button type='submit'>Submit Result</button>
      </form>
      {/* <StyledBtn txt={"Submit"} func={() => document.forms[0].submit()}/> */}
      <div>Your testInput: {testInput} and testNumber: {testNumber}</div>
    </div>
  )
}

