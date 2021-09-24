import React, { useEffect, useState } from 'react';
import StyledBtn from '../Shared/Button'
import { regValidate, enumToText } from '../Helpers/stringHelper'
import './Home.css'

export default function Home() {

  const [testInput, setTestInput] = useState("");
  const [testNumber, setTestNumber] = useState("");
  const [bloodTestResult, setBloodTestResult] = useState(null)
  const header = `Hello Heart <3`

  useEffect(() => {
    initBloodTestConfigData();
  },[])

  useEffect(() => {
    console.log("bloodTestResult: ", bloodTestResult)
  },[bloodTestResult])

  function onSubmit(e) {
    e.preventDefault();
    if(!testInput || !testNumber) return;
    if(!regValidate(testInput)) return;
    submitBloodTestData(testInput, testNumber)
    resetStates();
  }

  function resetStates() {
    setTestInput("")
    setTestNumber("");
  }

  async function initBloodTestConfigData() {
    const response = await fetch('bloodtest');
    const data = await response.json();
    console.log(response, data);
  }

  async function submitBloodTestData(testInput, testNumber) {
    const bloodTest = { TestInput: testInput.toUpperCase(), TestNumber: testNumber }
    const request = {
      method: 'POST',
      Accept: 'application/json',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(bloodTest)
    };
    fetch('bloodtest/setResults', request)
      .then(response => response.json())
      .then(data => setBloodTestResult(data));
  }

  return (
    <div className="home-container">
      <h3>{header}</h3> 
      <form className="input-test-form" onSubmit={onSubmit}>
        <label className="label-header">Testing Type: </label>
        <input className="input-field" type='text'value={testInput} onChange={e => setTestInput(e.target.value)}></input>
        <label className="label-header">Testing Numiric Result: </label>
        <input className="input-field" type='number' value={testNumber} onChange={e => setTestNumber(e.target.value)}></input>
        <button type='submit'>Submit Result</button>
      </form>
      {/* <StyledBtn txt={"Submit"} func={() => document.forms[0].submit()}/> */}
      { bloodTestResult ? <div>Your {bloodTestResult?.result || "Unkown"} Is {enumToText(bloodTestResult.resultEvaluation)}</div> : null }
    </div>
  )
}

