import { useState } from "react";
import { InputGroup, Form } from "react-bootstrap";


interface NumberInputProps {
  label: string,
  initialValue: number,
  onChange: (value: number) => void
};

export default function NumberInput({ label, initialValue, onChange }: NumberInputProps) {
  const [displayed, setDisplayed] = useState(initialValue.toString());

  const handleChange = (value: string) => {
    setDisplayed(value);
    onChange(Math.max(Number(value) || 0.0, 0.0));
  };

  return (
    <InputGroup className="mb-3">
      <InputGroup.Text>{label}</InputGroup.Text>
      <Form.Control type="number" value={displayed} onChange={e => handleChange(e.target.value)} />
    </InputGroup>
  );
}