export default function Home() {
  return (
    <>
      <h1>Currency converter</h1>
      <hr />
        <p>Available converters:</p>
      <ul>
        <li>Converter - Simple converter that uses latest available excange rates</li>
        <li>Historical converter - Converter that uses exchange rates from available historical exchange rate pool</li>
      </ul>
      <p>All data was collected from <a href="https://www.ecb.europa.eu" target="_blank">European Central Bank</a> website</p>
    </>
  )
}