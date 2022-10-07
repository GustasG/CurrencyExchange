import { Form, InputGroup } from "react-bootstrap";

interface DropDownProps {
  label: string,
  options: string[],
  value: string,
  onChange: (change: string) => void
};

export default function DropDown({ label, options, value, onChange }: DropDownProps) {
  return (
    <InputGroup className="mb-3">
      <InputGroup.Text>{label}</InputGroup.Text>
      <Form.Select value={value} onChange={(e) => onChange(e.target.value)}>
        {options.map((option, i) =>
          <option key={i}>{option}</option>
        )}
      </Form.Select>
  </InputGroup>
  );
}