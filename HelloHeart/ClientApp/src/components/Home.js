import React, { useEffect, useState } from 'react';
import { regValidate, enumToText } from '../Helpers/stringHelper'
import './Home.css'

export default function Home() {

  const [testInput, setTestInput] = useState("");
  const [testNumber, setTestNumber] = useState("");
  const [bloodTestResult, setBloodTestResult] = useState(null);
  const [loading, setLoading] = useState(false);
  const inputError = testInput && testInput.length > 1 && !regValidate(testInput);

  const header = `Hello Heart`
  const errorTxt = `Please avoid typing symbols, try again`
  const loaderTxt = `Calculating Results Please Wait...`;

  useEffect(() => {
    initBloodTestConfigData();
  },[])

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
    await fetch('bloodtest');
  }

  async function submitBloodTestData(testInput, testNumber) {
    const bloodTest = { TestInput: testInput, TestNumber: testNumber }
    const request = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(bloodTest)
    };
    setLoading(true);
    fetch('bloodtest/setResults', request)
      .then(response => response.json())
      .then(data => {
        setLoading(false);
        setBloodTestResult(data);
      });
  }

  function renderOutputStr(txtResult, numiricResult) {
    const resultState = enumToText(numiricResult);
    const officialTestName = txtResult;
    if(numiricResult == 0) return resultState;
    const bloodTest = `Your ${officialTestName} is `
    const resultColor = numiricResult == 1 ? "green": "black";
    return (
        <div>{bloodTest}<span style={{color: resultColor}}>{resultState}</span></div>
      )
  }

  return (
    <div className="home-container">
      <h3 className="home-header">{header}</h3> 
      
      <form className="input-test-form" onSubmit={onSubmit}>
        <label className="label-header">Testing Type: </label>
        <input className="input-field" type='text'value={testInput} onChange={e => setTestInput(e.target.value)} placeholder="what should we check today?"></input>
        <label className="label-header">Testing Numiric Result: </label>
        <input className="input-field" type='number' value={testNumber} onChange={e => setTestNumber(e.target.value)} placeholder={0}></input>
        <button type='submit'>Submit Result</button>
      </form>

      {inputError && <span className="error-line">{errorTxt}</span>}
      <span className="heart"></span>
      { loading && <div className="loader">{loaderTxt}</div>}
      { bloodTestResult && <div className="result-test-txt">{renderOutputStr(bloodTestResult.result, bloodTestResult.resultEvaluation)}</div>}
    </div>
  )
}

