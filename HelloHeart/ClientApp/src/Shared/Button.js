import React, { useState, useRef } from "react";
import "./Button.css";

export default function FancyBtn({ txt, func }) {
  const mouseTarget = useRef(null);
  const right = useRef(null);
  const left = useRef(null);
  const middle = useRef(null);
  const [className, setClassName] = useState("btn");

  const enterMiddle = () => {
    setClassName("btn");
  };

  const enterRight = () => {
    setClassName("btn-right");
  };

  const enterLeft = () => {
    setClassName("btn-left");
  };

  const onLeave = () => {
    setClassName("btn");
  };

  return (
      <div
        className={className}
        ref={mouseTarget}
        onMouseLeave={onLeave}
        onClick={() => func()}
      >
        <span className="btn-txt">{txt}</span>
        <div className="left" onMouseOver={enterLeft} ref={left}></div>
        <div className="middle" onMouseOver={enterMiddle} ref={middle}></div>
        <div className="right" ref={right} onMouseOver={enterRight}></div>
      </div>
  );
}
